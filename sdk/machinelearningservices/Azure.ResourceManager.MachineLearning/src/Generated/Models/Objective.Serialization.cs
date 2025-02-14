// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.MachineLearning.Models
{
    public partial class Objective : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("goal");
            writer.WriteStringValue(Goal.ToString());
            writer.WritePropertyName("primaryMetric");
            writer.WriteStringValue(PrimaryMetric);
            writer.WriteEndObject();
        }

        internal static Objective DeserializeObjective(JsonElement element)
        {
            Goal goal = default;
            string primaryMetric = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("goal"))
                {
                    goal = new Goal(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("primaryMetric"))
                {
                    primaryMetric = property.Value.GetString();
                    continue;
                }
            }
            return new Objective(goal, primaryMetric);
        }
    }
}
