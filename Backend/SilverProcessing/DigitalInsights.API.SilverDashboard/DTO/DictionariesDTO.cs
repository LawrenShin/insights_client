using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class DictionariesDTO
    {
        [JsonProperty(PropertyName = "addressType")]
        public EnumDTO[] AddressTypes { get; set; }

        [JsonProperty(PropertyName = "country")]
        public CountryDTO[] Countries { get; set; }

        [JsonProperty(PropertyName = "educationLevel")]
        public EnumDTO[] EducationLevels { get; set; }

        [JsonProperty(PropertyName = "educationSubject")]
        public EnumDTO[] EducationSubjects { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public EnumDTO[] Genders { get; set; }

        [JsonProperty(PropertyName = "industry")]
        public EnumDTO[] Industries { get; set; }

        [JsonProperty(PropertyName = "industryCode")]
        public EnumDTO[] IndustryCodes { get; set; }

        [JsonProperty(PropertyName = "race")]
        public EnumDTO[] Races { get; set; }

        [JsonProperty(PropertyName = "religion")]
        public EnumDTO[] Religions { get; set; }

        [JsonProperty(PropertyName = "roleType")]
        public EnumDTO[] RoleTypes { get; set; }
    }
}
