// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.RecoveryServicesSiteRecovery.Models
{
    /// <summary> Supported operating systems property. </summary>
    public partial class SupportedOSProperty
    {
        /// <summary> Initializes a new instance of SupportedOSProperty. </summary>
        internal SupportedOSProperty()
        {
            SupportedOS = new ChangeTrackingList<SupportedOSDetails>();
        }

        /// <summary> Initializes a new instance of SupportedOSProperty. </summary>
        /// <param name="instanceType"> The replication provider type. </param>
        /// <param name="supportedOS"> The list of supported operating systems. </param>
        internal SupportedOSProperty(string instanceType, IReadOnlyList<SupportedOSDetails> supportedOS)
        {
            InstanceType = instanceType;
            SupportedOS = supportedOS;
        }

        /// <summary> The replication provider type. </summary>
        public string InstanceType { get; }
        /// <summary> The list of supported operating systems. </summary>
        public IReadOnlyList<SupportedOSDetails> SupportedOS { get; }
    }
}
