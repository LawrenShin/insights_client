using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    class RoleDTO
    {
        public RoleDTO()
        {

        }

        public RoleDTO(Role source)
        {
            BaseSalary = source.BaseSalary;
            IsEffective = source.IsEffective;
            OtherIncentives = source.OtherIncentives;
            PersonId = source.PersonId;
            RoleType = (int)source.RoleType;
            Title = source.Title;
        }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("otherIncentves")]
        public double? OtherIncentives { get; set; }
        [JsonProperty("personId")]
        public int? PersonId { get; set; }
        [JsonProperty("roleType")]
        public int? RoleType { get; set; }
        [JsonProperty("baseSalary")]
        public double? BaseSalary { get; set; }
        [JsonProperty("isEffective")]
        public short? IsEffective { get; set; }
    }
}
