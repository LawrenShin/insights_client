using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class CompanyCountry
    {
        public int CompanyCountryId { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public bool LegalJurisdiction { get; set; }
        public string Ticker { get; set; }
        public string StockIndex { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
