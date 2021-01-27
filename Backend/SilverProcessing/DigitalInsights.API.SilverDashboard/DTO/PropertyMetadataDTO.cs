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
        [JsonIgnore]
        public string PropertyName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string FieldType { get; set; }
        public EntityMetadataDTO EntityMetadata { get; set; }
        public string Dictionary { get; set; }
        public string RangeLow { get; set; }
        public string RangeHigh { get; set; }
        public bool IsEditable { get; set; }
        public bool AllowsNull { get; set; }
        public int FieldOrder { get; set; }

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

        public bool ShouldSerializeChildEntityName()
        {
            return (FieldType == "Array" || FieldType == "NestedEntity") && EntityMetadata != null;
        }
    }
}
