using DigitalInsights.DB.Silver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class EntityMetadataDTO
    {
        public EntityMetadataDTO()
        {
        }

        public EntityMetadataDTO(string entityName, Dictionary<string, Dictionary<string, PropertyMetadata>> groups)
        {
            EntityName = entityName;

            PropertyMetadata = groups[entityName].Values.Select(x =>
                new PropertyMetadataDTO()
                {
                    AllowsNull = x.AllowsNull,
                    Description = x.Description,
                    Dictionary = x.DropDownDictionary,
                    DisplayName = x.FrontendName,
                    FieldType = x.FieldType.ToString(),
                    IsEditable = x.IsEditable,
                    RangeHigh = x.RangeHigh,
                    RangeLow = x.RangeLow,
                    PropertyName = x.PropertyName,
                    EntityMetadata = 
                        x.FieldType == DB.Common.Enums.FieldType.Array 
                            ? new EntityMetadataDTO(x.ChildrenEntityName, groups) 
                            : null
                }).ToArray();
        }

        [JsonProperty("entityName")]
        public string EntityName { get; set; }

        [JsonProperty("propertyMetadata")]
        public PropertyMetadataDTO[] PropertyMetadata { get; set; }
    }
}
