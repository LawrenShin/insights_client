using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class AddressDTO
    {
        public AddressDTO()
        {

        }

        public AddressDTO(Address source)
        {
            
            if (source != null)
            {
                AddressType = (int)source.AddressType;
                City = source.City;
                Country = source.CountryId;
                IsoCode = source.Country.ISOCode;
                IsEditable = source.IsEditable;
                PostCode = source.PostCode;
                State = source.State;
                StreetOne = source.StreetOne;
                StreetTwo = source.StreetTwo;
            }
        }

        public int? Country { get; set; }
        public string IsoCode { get; set; }
        public bool IsEditable { get; set; }
        public string PostCode { get; set; }
        public string State { get; set; }
        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public int AddressType { get; set; }
        public string City { get; set; }
    }
}