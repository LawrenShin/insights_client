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
                IsEditable = source.IsEditable;
                PostCode = source.PostCode;
                State = source.State;
                StreetOne = source.StreetOne;
                StreetTwo = source.StreetTwo;
            }
        }

        public int? Country { get; private set; }
        public bool IsEditable { get; private set; }
        public string PostCode { get; private set; }
        public string State { get; private set; }
        public string StreetOne { get; private set; }
        public string StreetTwo { get; private set; }
        public int AddressType { get; private set; }
        public string City { get; private set; }
    }
}