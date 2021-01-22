using DigitalInsights.DB.Silver.Entities;
using DigitalInsights.DB.Silver.Entities.CompanyData;
using Newtonsoft.Json;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyCountryDTO
    {
        public CompanyCountryDTO()
        {

        }
        public CompanyCountryDTO(CompanyCountry source)
        {
            CountryId = source.CountryId;
            Ticker = source.Ticker;
            IsoCode = source.Country.ISOCode;
        }

        public int CountryId { get; private set; }
        public string Ticker { get; private set; }
        public string IsoCode { get; private set; }
    }
}
