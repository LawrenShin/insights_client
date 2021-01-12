using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Services
{
    internal class DictionaryService
    {
        SilverContext silverContext;

        public DictionaryService(SilverContext context)
        {
            silverContext = context;
        }

        public RoleTypeDTO[] GetRoleTypes()
        {
            return Enum.GetValues<RoleType>().Select(x => new RoleTypeDTO { Id = (int)x, Name = x.ToString() }).ToArray();
        }

        public Country[] GetCountries()
        {
            return silverContext.Countries.ToArray();
        }

        public Industry[] GetIndustries()
        {
            return silverContext.Industries.ToArray();
        }
    }
}
