function Get-NamepspacesFromDll($dllPath) {
    $file = [System.IO.File]::OpenRead($dllPath)
    $namespaces = @()
    try {
        # Use to parse the namespaces out from the dll file.
        $pe = [System.Reflection.PortableExecutable.PEReader]::new($file)
        try {
            $meta = [System.Reflection.Metadata.PEReaderExtensions]::GetMetadataReader($pe)
            foreach ($typeHandle in $meta.TypeDefinitions) {
                $type = $meta.GetTypeDefinition($typeHandle)
                $attr = $type.Attributes
                if ($attr -band 'Public' -and !$type.IsNested) {
                    $namespaces += $meta.GetString($type.Namespace)
                }
            }
        } finally {
            $pe.Dispose()
        }
    } finally {
        $file.Dispose()
    }
    return $namespaces | Sort-Object -Unique
}

function DownloadNugetPackage($package, $version, $destination) {
    # $PackageSourceOverride is a global variable provided in
    # Update-DocsMsPackages.ps1. Its value can set a "customSource" property.
    # If it is empty then the property is not overridden.
    $customPackageSource = Get-Variable -Name 'PackageSourceOverride' -ValueOnly -ErrorAction 'Ignore'
    $downloadUri = ""
    if (!$customPackageSource) {
        # Download package from nuget
        Write-Host "Download from nuget: https://www.nuget.org/api/v2/package/$package/$version"
        $downloadUri = "https://www.nuget.org/api/v2/package/$package/$version"
    }
    else{
        # Download package from devop public feeds
        Write-Host "Download from public feeds: https://pkgs.dev.azure.com/azure-sdk/public/_apis/packaging/feeds/azure-sdk-for-net/nuget/packages/$package/versions/$version/content?api-version=6.0-preview.1"
        $downloadUri = "https://pkgs.dev.azure.com/azure-sdk/public/_apis/packaging/feeds/azure-sdk-for-net/nuget/packages/$package/versions/$version/content?api-version=6.0-preview.1"
    }

    # Invoke the download link and store it into destination
    Invoke-WebRequest $downloadUri -OutFile $destination
}

function Fetch-NamespacesFromNupkg ($package, $version) {
    $tempLocation = (Join-Path ([System.IO.Path]::GetTempPath()) "extractNupkg")
    if (Test-Path $tempLocation) {
        Remove-Item $tempLocation/* -Recurse -Force 
    }
    else {
        New-Item -ItemType Directory -Path $tempLocation -Force | Out-Null
    }
    $nupkgFilePath = "$tempLocation/$package-$version.nupkg"
    Write-Host "Downloading nupkg packge to $nupkgFilePath ...."
    DownloadNugetPackage -package $package -version $version -destination $nupkgFilePath
    Write-Host "Unzipping nupkg..."
    # Extracting nupkg to ddl
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::ExtractToDirectory($nupkgFilePath, $tempLocation)
    # .NET core includes multiple target framework. We currently have the same namespaces for different framework. 
    # Will use whatever the first dll file.
    Write-Host "Searching ddl file..."
    $dllFile = @()
    if (Test-Path $tempLocation/lib/netstandard2.0/){
        $dllFile = @(Get-ChildItem "$tempLocation/lib/netstandard2.0/*" -Filter '*.dll' -Recurse)
    }
    if (!$dllFile -or ($dllFile.Count -gt 1)) {
        Write-Warning "Can't find any dll file from $nupkgFilePath with target netstandard2.0."
        $dllFiles = Get-ChildItem "$tempLocation/lib/*" -Filter '*.dll' -Recurse
        if (!$dllFiles) {
            Write-Error "Can't find any dll file from $nupkgFilePath."
            return @()
        }
        $dllFile = $dllFiles[0]
    }
    Write-Host "Dll file found: $($dllFile.FullName)"
    $namespaces = Get-NamepspacesFromDll $dllFile.FullName
    if (!$namespaces) {
        Write-Error "Can't find namespaces from dll file $($dllFile.FullName)."
        return @()
    }
    return $namespaces
}

function Get-dotnet-OnboardedDocsMsPackages($DocRepoLocation) {
    $packageOnboardingFiles = @(
        "$DocRepoLocation/bundlepackages/azure-dotnet.csv",
        "$DocRepoLocation/bundlepackages/azure-dotnet-preview.csv",
        "$DocRepoLocation/bundlepackages/azure-dotnet-legacy.csv")

    $onboardedPackages = @{}
    foreach ($file in $packageOnboardingFiles) {
        $onboardingSpec = Get-Content $file
        foreach ($spec in $onboardingSpec) {
            $packageInfo = $spec -split ","
            if (!$packageInfo -or ($packageInfo.Count -lt 2)) {
                Write-Error "Please check the package info in csv file $file. Please have at least one package and follow the format {name index, package name, version(optional)}"
                return $null
            }
            $packageName = $packageInfo[1].Trim() -replace "\[.*\](.*)", '$1'
            $onboardedPackages[$packageName] = $null
        }
    }
    return $onboardedPackages
}

function GetPackageReadmeName($packageMetadata) {
    # Fallback to get package-level readme name if metadata file info does not exist
    $packageLevelReadmeName = $packageMetadata.Package.ToLower().Replace('azure.', '')
  
    # If there is a metadata json for the package use the DocsMsReadmeName from
    # the metadata function
    if ($packageMetadata.PSObject.Members.Name -contains "FileMetadata") {
      $readmeMetadata = &$GetDocsMsMetadataForPackageFn -PackageInfo $packageMetadata.FileMetadata
      $packageLevelReadmeName = $readmeMetadata.DocsMsReadMeName
    }
    return $packageLevelReadmeName
  }
  
function Get-dotnet-DocsMsTocData($packageMetadata, $docRepoLocation) {
    $packageLevelReadmeName = GetPackageReadmeName -packageMetadata $packageMetadata
    $packageTocHeader = $packageMetadata.Package
    if ($packageMetadata.DisplayName) {
      $packageTocHeader = $packageMetadata.DisplayName
    }
    $children = @()
    # Children here combine namespaces in both preview and GA.
    if($packageMetadata.VersionPreview) {
        $children += Get-Toc-Children -package $packageMetadata.Package -version $packageMetadata.VersionPreview `
            -docRepoLocation $docRepoLocation -folderName "preview"
    }
    if($packageMetadata.VersionGA) {
        $children += Get-Toc-Children -package $packageMetadata.Package -version $packageMetadata.VersionGA `
            -docRepoLocation $docRepoLocation -folderName "latest"
    }
    $children = @($children | Sort-Object -Unique)
    # Write-Host $children
    if (!$children) {
        if ($packageMetadata.VersionPreview) {
            Write-Host "Did not find the package namespaces for $($packageMetadata.Package):$($packageMetadata.VersionPreview)"
        }
        if ($packageMetadata.VersionGA) {
            Write-Host "Did not find the package namespaces for $($packageMetadata.Package):$($packageMetadata.VersionGA)"
        }
    }
    $output = [PSCustomObject]@{
      PackageLevelReadmeHref = "~/api/overview/azure/{moniker}/$packageLevelReadmeName-readme.md"
      PackageTocHeader       = $packageTocHeader
      TocChildren            = $children
    }
  
    return $output
}


# This is a helper function which fetches the dotnet package namespaces from nupkg.
# Here are the major workflow:
# 1. Read the ${package}.json under /metadata folder. If Namespaces exists and version match, return the array. 
# 2. If the json file exist but version mismatch, fetch the namespaces from nuget and udpate the json namespaces property.
# 3. If file not found, then fetch namespaces from nuget and create new package.json file with very basic info, like package name, version, namespaces. 
function Get-Toc-Children($package, $version, $docRepoLocation, $folderName) {
    # Looking for the txt
    $packageJsonPath = "$docRepoLocation/metadata/$folderName/$package.json" 
    $packageJsonObject = [PSCustomObject]@{
        Name = $package
        Version = $version
        Namespaces = @()
    }
    # We will download package and parse out namespaces for the following cases.
    # 1. Package json file doesn't exist. Create a new json file with $packageJsonObject
    # 2. Package json file exists, but no Namespaces property. Add the Namespaces property with right values.
    # 3. Both package json file and Namespaces property exist, but the version mismatch. Update the nameapace values.
    $jsonExists = Test-Path $packageJsonPath
    $namespacesExist = $false
    $versionMismatch = $false
    $packageJson = $null
    # Add backup if no namespaces fetched out from new packages.
    $originalNamespaces = @()
    # Collect the information and return the booleans of updates or not.
    if ($jsonExists) {
        $packageJson = Get-Content $packageJsonPath | ConvertFrom-Json
        $namespacesExist = $packageJson.PSObject.Members.Name -contains "Namespaces"
        if ($namespacesExist) {
            # Fallback to original ones if failed to fetch updated namespaces.
            $originalNamespaces = $packageJson.Namespaces
        }
        $actualVersion = $packageJson.Version
        $versionMismatch = $version -ne $actualVersion
    }
    if ($jsonExists -and $namespacesExist -and !$versionMismatch) {
        return $originalNamespaces
    }
    if (!$jsonExists) {
        New-Item $packageJsonPath -Type File -Force
    }
    $namespaces = Fetch-NamespacesFromNupkg -package $package -version $version
    if (!$namespaces) {
        $namespaces = $originalNamespaces
    }
    if (!$packageJson) {
        $packageJsonObject.Namespaces = $namespaces
        $packageJson = ($packageJsonObject | ConvertTo-Json | ConvertFrom-Json)
    }
    else {
        $packageJson = $packageJson | Add-Member -MemberType NoteProperty -Name Namespaces -Value $namespaces -PassThru
    }
    # Keep the json file up to date.
    try {
        Set-Content $packageJsonPath -Value ($packageJson | ConvertTo-Json)
        return $namespaces
    }
    catch {
        Write-Error "NO namespaces fetched from $package-$version. "
        return @()
    }
}
