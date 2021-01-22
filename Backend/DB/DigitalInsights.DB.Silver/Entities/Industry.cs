using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Industry
    {
        public Industry()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
