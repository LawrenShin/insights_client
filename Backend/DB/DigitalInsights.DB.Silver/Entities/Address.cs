using DigitalInsights.DB.Common.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Address
    {
        public int Id { get; set; }
        public AddressType AddressType { get; set; }
        public bool IsEditable { get; set; }
        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? CountryId { get; set; }

        public int CompanyId { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }

        public virtual Company Company { get; set; }
    }
}
