// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using Azure.Core;

namespace Azure.ResourceManager.SecurityCenter.Models
{
    /// <summary> The target Event Hub to which event data will be exported. To learn more about Microsoft Defender for Cloud continuous export capabilities, visit https://aka.ms/ASCExportLearnMore. </summary>
    public partial class AutomationActionEventHub : AutomationAction
    {
        /// <summary> Initializes a new instance of AutomationActionEventHub. </summary>
        public AutomationActionEventHub()
        {
            ActionType = ActionType.EventHub;
        }

        /// <summary> Initializes a new instance of AutomationActionEventHub. </summary>
        /// <param name="actionType"> The type of the action that will be triggered by the Automation. </param>
        /// <param name="eventHubResourceId"> The target Event Hub Azure Resource ID. </param>
        /// <param name="sasPolicyName"> The target Event Hub SAS policy name. </param>
        /// <param name="connectionString"> The target Event Hub connection string (it will not be included in any response). </param>
        internal AutomationActionEventHub(ActionType actionType, ResourceIdentifier eventHubResourceId, string sasPolicyName, string connectionString) : base(actionType)
        {
            EventHubResourceId = eventHubResourceId;
            SasPolicyName = sasPolicyName;
            ConnectionString = connectionString;
            ActionType = actionType;
        }

        /// <summary> The target Event Hub Azure Resource ID. </summary>
        public ResourceIdentifier EventHubResourceId { get; set; }
        /// <summary> The target Event Hub SAS policy name. </summary>
        public string SasPolicyName { get; }
        /// <summary> The target Event Hub connection string (it will not be included in any response). </summary>
        public string ConnectionString { get; set; }
    }
}
