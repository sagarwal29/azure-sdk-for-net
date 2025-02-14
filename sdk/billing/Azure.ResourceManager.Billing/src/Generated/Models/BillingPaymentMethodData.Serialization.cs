// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager.Billing.Models;
using Azure.ResourceManager.Models;

namespace Azure.ResourceManager.Billing
{
    public partial class BillingPaymentMethodData : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("properties");
            writer.WriteStartObject();
            if (Optional.IsDefined(Family))
            {
                writer.WritePropertyName("family");
                writer.WriteStringValue(Family.Value.ToString());
            }
            if (Optional.IsCollectionDefined(Logos))
            {
                writer.WritePropertyName("logos");
                writer.WriteStartArray();
                foreach (var item in Logos)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsDefined(Status))
            {
                writer.WritePropertyName("status");
                writer.WriteStringValue(Status.Value.ToString());
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        internal static BillingPaymentMethodData DeserializeBillingPaymentMethodData(JsonElement element)
        {
            ResourceIdentifier id = default;
            string name = default;
            ResourceType type = default;
            Optional<SystemData> systemData = default;
            Optional<PaymentMethodFamily> family = default;
            Optional<string> type0 = default;
            Optional<string> accountHolderName = default;
            Optional<string> expiration = default;
            Optional<string> lastFourDigits = default;
            Optional<string> displayName = default;
            Optional<IList<PaymentMethodLogo>> logos = default;
            Optional<PaymentMethodStatus> status = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = new ResourceIdentifier(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    type = new ResourceType(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("systemData"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    systemData = JsonSerializer.Deserialize<SystemData>(property.Value.ToString());
                    continue;
                }
                if (property.NameEquals("properties"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        if (property0.NameEquals("family"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            family = new PaymentMethodFamily(property0.Value.GetString());
                            continue;
                        }
                        if (property0.NameEquals("type"))
                        {
                            type0 = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("accountHolderName"))
                        {
                            accountHolderName = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("expiration"))
                        {
                            expiration = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("lastFourDigits"))
                        {
                            lastFourDigits = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("displayName"))
                        {
                            displayName = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("logos"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<PaymentMethodLogo> array = new List<PaymentMethodLogo>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(PaymentMethodLogo.DeserializePaymentMethodLogo(item));
                            }
                            logos = array;
                            continue;
                        }
                        if (property0.NameEquals("status"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            status = new PaymentMethodStatus(property0.Value.GetString());
                            continue;
                        }
                    }
                    continue;
                }
            }
            return new BillingPaymentMethodData(id, name, type, systemData.Value, Optional.ToNullable(family), type0.Value, accountHolderName.Value, expiration.Value, lastFourDigits.Value, displayName.Value, Optional.ToList(logos), Optional.ToNullable(status));
        }
    }
}
