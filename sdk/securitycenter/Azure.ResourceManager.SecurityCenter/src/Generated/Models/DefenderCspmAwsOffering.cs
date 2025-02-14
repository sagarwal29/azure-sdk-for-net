// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.SecurityCenter.Models
{
    /// <summary> The CSPM P1 for Aws offering. </summary>
    public partial class DefenderCspmAwsOffering : CloudOffering
    {
        /// <summary> Initializes a new instance of DefenderCspmAwsOffering. </summary>
        public DefenderCspmAwsOffering()
        {
            OfferingType = OfferingType.DefenderCspmAws;
        }

        /// <summary> Initializes a new instance of DefenderCspmAwsOffering. </summary>
        /// <param name="offeringType"> The type of the security offering. </param>
        /// <param name="description"> The offering description. </param>
        /// <param name="vmScanners"> The Microsoft Defender for Server VM scanning configuration. </param>
        internal DefenderCspmAwsOffering(OfferingType offeringType, string description, DefenderCspmAwsOfferingVmScanners vmScanners) : base(offeringType, description)
        {
            VmScanners = vmScanners;
            OfferingType = offeringType;
        }

        /// <summary> The Microsoft Defender for Server VM scanning configuration. </summary>
        public DefenderCspmAwsOfferingVmScanners VmScanners { get; set; }
    }
}
