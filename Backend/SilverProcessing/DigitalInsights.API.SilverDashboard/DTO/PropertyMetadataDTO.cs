using DigitalInsights.DB.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class PropertyMetadataDTO
    {
        public PropertyMetadataDTO()
        {

        }

        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fieldType")]
        public string FieldType { get; set; }

        [JsonProperty("entityMetadata")]
        public EntityMetadataDTO EntityMetadata { get; set; }

        [JsonProperty("dictionary")]
        public string Dictionary { get; set; }

        [JsonProperty("rangeLow")]
        public string RangeLow { get; set; }

        [JsonProperty("rangeHigh")]
        public string RangeHigh { get; set; }

        [JsonProperty("isEditable")]
        public bool IsEditable { get; set; }

        [JsonProperty("allowsNull")]
        public bool AllowsNull { get; set; }

        public bool ShouldSerializeDictionary()
        {
            return FieldType == "DropDown";
        }

        public bool ShouldSerializeRangeLow()
        {
            return IsEditable && !string.IsNullOrEmpty(RangeLow)
                && (FieldType == "Integer"
                || FieldType == "Float"
                || FieldType == "Percentage");
        }

        public bool ShouldSerializeRangeHigh()
        {
            return IsEditable && !string.IsNullOrEmpty(RangeHigh)
                && (FieldType == "Integer"
                || FieldType == "Float"
                || FieldType == "Percentage");
        }

        public bool ShouldSerializeAllowsNull()
        {
            return IsEditable;
        }

        public bool ShouldSerializeDisplayName()
        {
            return !string.IsNullOrEmpty(DisplayName);
        }

        public bool ShouldSerializeDescription()
        {
            return !string.IsNullOrEmpty(Description);
        }

        public bool ShouldSerializeEntityMetadata()
        {
            return FieldType == "Array" && EntityMetadata != null;
        }
    }
}
