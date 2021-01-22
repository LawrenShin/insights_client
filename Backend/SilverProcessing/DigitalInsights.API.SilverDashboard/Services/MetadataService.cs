using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.API.SilverDashboard.Helpers;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Services
{
    class MetadataService
    {
        private SilverContext silverContext;

        public MetadataService(SilverContext silverContext)
        {
            this.silverContext = silverContext;
        }

        internal EntityMetadataDTO[] GetUIMetadata(params string[] entities)
        {
            var groups = PropertyMetadataStorage.CurrentPropertyMetadata;

            return entities.Select(x => new EntityMetadataDTO(x, groups)).ToArray();
        }
    }
}
