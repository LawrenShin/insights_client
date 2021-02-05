using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class RoleDTO
    {
        public RoleDTO()
        {

        }

        public RoleDTO(Role source)
        {
            BaseSalary = source.BaseSalary;
            JobTenure = source.JobTenure;
            OtherIncentives = source.OtherIncentives;
            CompanyId = source.CompanyId;
            PersonId = source.PersonId;
            RoleType = (int)source.RoleType;
            Title = source.Title;
        }

        public string Title { get; set; }
        public double? OtherIncentives { get; set; }
        public int? CompanyId { get; set; }
        public int? PersonId { get; set; }
        public int? RoleType { get; set; }
        public double? BaseSalary { get; set; }
        public int? JobTenure { get; set; }
    }
}
