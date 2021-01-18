using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class PersonDTO
    {
        public PersonDTO()
        {
            Countries = new int[0];
        }

        public PersonDTO(Person source)
        {
            Countries = source.PersonCountries.Select(x => x.CountryId).OrderBy(x => x).ToArray();
            Age = source.Age;
            BirthYear = source.BirthYear;
            EducationInstitute = source.EducationInstitute;
            EducationLevel = source.EducationLevel.HasValue ? (int)source.EducationLevel : null;
            EducationSubject = source.EducationSubject.HasValue ? (int)source.EducationSubject : null;
            Gender = source.Gender.HasValue ? (int)source.Gender : null;
            HasKids = source.HasKids;
            Id = source.Id;
            MaritalStatus = source.Married.HasValue ? (int)source.Married : null;
            Name = source.Name;
            Picture = source.Picture;
            Religion = source.Religion.HasValue ? (int)source.Religion : null; ;
            Sexuality = source.Sexuality;
            Urban = source.Urban;
            VisibleDisability = source.VisibleDisability;
            Website = source.Website;
        }


        [JsonProperty("countries")]
        public int[] Countries { get; set; }
        [JsonProperty("age")]
        public short? Age { get; set; }
        [JsonProperty("birthYear")]
        public short? BirthYear { get; set; }
        [JsonProperty("educationInstitute")]
        public string EducationInstitute { get; set; }
        [JsonProperty("educationLevel")]
        public int? EducationLevel { get; set; }
        [JsonProperty("educationSubject")]
        public int? EducationSubject { get; set; }
        [JsonProperty("gender")]
        public int? Gender { get; set; }
        [JsonProperty("hasKids")]
        public short? HasKids { get; set; }
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("maritalStatus")]
        public int? MaritalStatus { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("picture")]
        public string Picture { get; set; }
        [JsonProperty("religion")]
        public int? Religion { get; set; }
        [JsonProperty("sexuality")]
        public string Sexuality { get; set; }
        [JsonProperty("livesInUrbanArea")]
        public int? Urban { get; set; }
        [JsonProperty("visibleDisability")]
        public string VisibleDisability { get; set; }
        [JsonProperty("corporateWebsitePersonalPageUrl")]
        public string Website { get; set; }
    }
}
