using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CompanyData
{
    public partial class CompanyCountry
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public string Ticker { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
