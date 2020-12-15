using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class CompanyCountry
    {
        public int CompanyCountryId { get; set; }
        public int? CompanyId { get; set; }
        public int? CountryId { get; set; }
        public string CompanyCountryLegalOperation { get; set; }
        public DateTime CompanyCountryEffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
    }
}
