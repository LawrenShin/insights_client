using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class PersonCountry
    {
        public int PersonCountryId { get; set; }
        public int? PersonId { get; set; }
        public int? CountryId { get; set; }
        public DateTime PersonCountryEffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
        public virtual Person Person { get; set; }
    }
}
