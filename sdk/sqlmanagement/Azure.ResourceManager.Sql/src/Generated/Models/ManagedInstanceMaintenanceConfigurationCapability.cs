// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.Sql.Models
{
    /// <summary> The maintenance configuration capability. </summary>
    public partial class ManagedInstanceMaintenanceConfigurationCapability
    {
        /// <summary> Initializes a new instance of ManagedInstanceMaintenanceConfigurationCapability. </summary>
        internal ManagedInstanceMaintenanceConfigurationCapability()
        {
        }

        /// <summary> Initializes a new instance of ManagedInstanceMaintenanceConfigurationCapability. </summary>
        /// <param name="name"> Maintenance configuration name. </param>
        /// <param name="status"> The status of the capability. </param>
        /// <param name="reason"> The reason for the capability not being available. </param>
        internal ManagedInstanceMaintenanceConfigurationCapability(string name, SqlCapabilityStatus? status, string reason)
        {
            Name = name;
            Status = status;
            Reason = reason;
        }

        /// <summary> Maintenance configuration name. </summary>
        public string Name { get; }
        /// <summary> The status of the capability. </summary>
        public SqlCapabilityStatus? Status { get; }
        /// <summary> The reason for the capability not being available. </summary>
        public string Reason { get; }
    }
}
