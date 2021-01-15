using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    class CompanyCountryDTO
    {
        public CompanyCountryDTO()
        {

        }
        public CompanyCountryDTO(CompanyCountry source)
        {
            CountryId = source.CountryId;
            IsPrimary = source.IsPrimary;
            LegalJurisdiction = source.LegalJurisdiction;
            StockIndex = source.StockIndex;
            Ticker = source.Ticker;
        }

        [JsonProperty("countryId")]
        public int? CountryId { get; private set; }
        [JsonProperty("isPrimary")]
        public bool? IsPrimary { get; private set; }
        [JsonProperty("legalJurisdiction")]
        public bool? LegalJurisdiction { get; private set; }
        [JsonProperty("stockIndex")]
        public string StockIndex { get; private set; }
        [JsonProperty("ticker")]
        public string Ticker { get; private set; }
    }
}
