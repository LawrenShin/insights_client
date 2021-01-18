using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class AddressDTO
    {
        public AddressDTO()
        {

        }

        public AddressDTO(Address address, bool? editable)
        {
            Editable = editable;
            if (address != null)
            {
                AddressLine = address.AddressLine;
                AddressNumber = address.AddressNumber;
                City = address.City;
                CountryId = address.CountryId;
                PostalCode = address.PostalCode;
                Region = address.Region;
            }
        }

        [JsonProperty("editable")]
        public bool? Editable { get; private set; }
        [JsonProperty("addressLine")]
        public string AddressLine { get; private set; }
        [JsonProperty("addressNumber")]
        public string AddressNumber { get; private set; }
        [JsonProperty("city")]
        public string City { get; private set; }
        [JsonProperty("countryId")]
        public int? CountryId { get; private set; }
        [JsonProperty("postalCode")]
        public string PostalCode { get; private set; }
        [JsonProperty("region")]
        public string Region { get; private set; }
    }
}