using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class PaginationDTO
    {
        public PaginationDTO()
        {

        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
