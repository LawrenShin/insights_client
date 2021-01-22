using DigitalInsights.DB.Silver.Entities;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class PersonNationalityDTO
    {
        public PersonNationalityDTO(PersonNationality source)
        {
            Country = source.CountryId;
        }
        public int Country { get; private set; }
    }
}