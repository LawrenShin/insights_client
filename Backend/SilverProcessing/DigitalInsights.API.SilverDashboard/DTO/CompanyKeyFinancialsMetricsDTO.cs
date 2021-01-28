using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyKeyFinancialsMetricsDTO
    {
        public CompanyKeyFinancialsMetricsDTO()
        {

        }
        public CompanyKeyFinancialsMetricsDTO(CompanyKeyFinancialsMetrics source)
        {
            Employees = source.Employees;
            OperatingRevenue = source.OperatingRevenue;
            TotalAssets = source.TotalAssets;
        }

        public int? Employees { get; set; }
        public double? OperatingRevenue { get; set; }
        public int? TotalAssets { get; set; }
    }
}
