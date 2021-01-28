using DigitalInsights.DB.Silver.Entities;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class PersonNationalityDTO
    {
        public PersonNationalityDTO()
        {

        }
        public PersonNationalityDTO(PersonNationality source)
        {
            Country = source.CountryId;
            IsoCode = source.Country.ISOCode;
        }
        public int Country { get; set; }

        public string IsoCode { get; set; }
    }
}