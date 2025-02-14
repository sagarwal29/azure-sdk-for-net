// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.SecurityInsights.Models
{
    /// <summary> Threat Intelligence Platforms data connector check requirements. </summary>
    public partial class TICheckRequirements : DataConnectorsCheckRequirements
    {
        /// <summary> Initializes a new instance of TICheckRequirements. </summary>
        public TICheckRequirements()
        {
            Kind = DataConnectorKind.ThreatIntelligence;
        }

        /// <summary> The tenant id to connect to, and get the data from. </summary>
        public Guid? TenantId { get; set; }
    }
}
