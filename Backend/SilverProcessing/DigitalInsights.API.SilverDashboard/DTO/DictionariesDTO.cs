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
        [JsonProperty(PropertyName = "addressTypes")]
        public EnumDTO[] AddressTypes { get; set; }

        [JsonProperty(PropertyName = "countries")]
        public CountryDTO[] Countries { get; set; }

        [JsonProperty(PropertyName = "educationLevels")]
        public EnumDTO[] EducationLevels { get; set; }

        [JsonProperty(PropertyName = "educationSubjects")]
        public EnumDTO[] EducationSubjects { get; set; }

        [JsonProperty(PropertyName = "genders")]
        public EnumDTO[] Genders { get; set; }

        [JsonProperty(PropertyName = "industries")]
        public EnumDTO[] Industries { get; set; }

        [JsonProperty(PropertyName = "industryCodes")]
        public EnumDTO[] IndustryCodes { get; set; }

        [JsonProperty(PropertyName = "races")]
        public EnumDTO[] Races { get; set; }

        [JsonProperty(PropertyName = "religions")]
        public EnumDTO[] Religions { get; set; }

        [JsonProperty(PropertyName = "roleTypes")]
        public EnumDTO[] RoleTypes { get; set; }
    }
}
