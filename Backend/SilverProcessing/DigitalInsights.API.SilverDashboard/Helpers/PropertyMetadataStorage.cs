using DigitalInsights.DB.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DigitalInsights.API.SilverDashboard.Helpers
{
    public static class PropertyMetadataStorage
    {
        private static ThreadLocal<Dictionary<string, Dictionary<string, PropertyMetadata>>> localMetadataStorage =
            new ThreadLocal<Dictionary<string, Dictionary<string, PropertyMetadata>>>(() =>
            {
                var context = new SilverContext();
                return context.PropertyMetadata.ToList().GroupBy(x => x.EntityName)
                    .ToDictionary(
                        x => x.Key, 
                        x => x.ToDictionary(y => y.PropertyName, y => y, StringComparer.OrdinalIgnoreCase), 
                        StringComparer.OrdinalIgnoreCase);
            });

        public static Dictionary<string, Dictionary<string, PropertyMetadata>> CurrentPropertyMetadata
        {
            get
            {
                return localMetadataStorage.Value;
            }
        }
    }
}
