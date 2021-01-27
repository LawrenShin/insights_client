using Newtonsoft.Json;
using System;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    internal class UIMetadataDTOConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UIMetadataDTO);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ui = (UIMetadataDTO)value;
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();

            foreach(var metadata in ui.Entities)
            {
                writer.WritePropertyName(metadata.EntityName.Substring(0, 1).ToLower() + metadata.EntityName.Substring(1));
                writer.WriteStartObject();
                foreach(var property in metadata.PropertyMetadata)
                {
                    WritePropertyMetadata(writer, property);
                }
                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }

        private static void WritePropertyMetadata(JsonWriter writer, PropertyMetadataDTO property)
        {
            writer.WritePropertyName(property.PropertyName.Substring(0, 1).ToLower() + property.PropertyName.Substring(1));
            writer.WriteStartObject();

            if (property.ShouldSerializeDisplayName())
            {
                writer.WritePropertyName("displayName");
                writer.WriteValue(property.DisplayName);
            }
            if (property.ShouldSerializeDescription())
            {
                writer.WritePropertyName("description");
                writer.WriteValue(property.Description);
            }

            writer.WritePropertyName("fieldType");
            writer.WriteValue(property.FieldType);

            writer.WritePropertyName("fieldOrder");
            writer.WriteValue(property.FieldOrder);

            if (property.ShouldSerializeAllowsNull())
            {
                writer.WritePropertyName("allowsNull");
                writer.WriteValue(property.AllowsNull);
            }

            writer.WritePropertyName("isEditable");
            writer.WriteValue(property.IsEditable);

            if (property.ShouldSerializeDictionary())
            {
                writer.WritePropertyName("dictionary");
                writer.WriteValue(property.Dictionary);
            }

            if (property.ShouldSerializeRangeLow())
            {
                writer.WritePropertyName("rangeLow");
                writer.WriteValue(property.RangeLow);
            }
            if (property.ShouldSerializeRangeHigh())
            {
                writer.WritePropertyName("rangeHigh");
                writer.WriteValue(property.RangeHigh);
            }

            if (property.ShouldSerializeChildEntityName())
            {
                writer.WritePropertyName("meta");
                writer.WriteStartObject();
                foreach (var childProperty in property.EntityMetadata.PropertyMetadata)
                {
                    WritePropertyMetadata(writer, childProperty);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}