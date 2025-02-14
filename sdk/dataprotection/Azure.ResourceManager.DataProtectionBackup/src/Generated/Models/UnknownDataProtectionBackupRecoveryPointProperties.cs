// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.DataProtectionBackup.Models
{
    /// <summary> The UnknownDataProtectionBackupRecoveryPointProperties. </summary>
    internal partial class UnknownDataProtectionBackupRecoveryPointProperties : DataProtectionBackupRecoveryPointProperties
    {
        /// <summary> Initializes a new instance of UnknownDataProtectionBackupRecoveryPointProperties. </summary>
        /// <param name="objectType"></param>
        internal UnknownDataProtectionBackupRecoveryPointProperties(string objectType) : base(objectType)
        {
            ObjectType = objectType ?? "Unknown";
        }
    }
}
