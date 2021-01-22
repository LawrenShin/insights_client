using DigitalInsights.API.SilverDashboard.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalInsights.API.SilverDashboard
{
    public class MetadataBasedContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> result =
                base.CreateProperties(type, memberSerialization);

            foreach(var property in result)
            {
                property.PropertyName = property.PropertyName.Substring(0, 1).ToLower() + property.PropertyName.Substring(1);
            }

            if (type.Name.EndsWith("DTO"))
            {
                var entityName = type.Name.Replace("DTO", "");

                if(PropertyMetadataStorage.CurrentPropertyMetadata.ContainsKey(entityName))
                {
                    result = result.Where(x => PropertyMetadataStorage.CurrentPropertyMetadata[entityName].ContainsKey(x.PropertyName)).ToList();
                }
            }

            return result;
        }
    }
}