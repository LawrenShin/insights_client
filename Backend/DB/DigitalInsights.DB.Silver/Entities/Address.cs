using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Address
    {
        public Address()
        {
            CompanyPublicDatumHqAddresses = new HashSet<CompanyPublicData>();
            CompanyPublicDatumLegalAddresses = new HashSet<CompanyPublicData>();
        }

        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string AddressNumber { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<CompanyPublicData> CompanyPublicDatumHqAddresses { get; set; }
        public virtual ICollection<CompanyPublicData> CompanyPublicDatumLegalAddresses { get; set; }
    }
}
