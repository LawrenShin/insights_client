using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Address
    {
        public Address()
        {
            CompanyHqs = new HashSet<Company>();
            CompanyLegals = new HashSet<Company>();
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string AddressNumber { get; set; }
        public int? CountryId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Company> CompanyHqs { get; set; }
        public virtual ICollection<Company> CompanyLegals { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
