using DigitalInsights.API.SilverDashboard.Helpers;
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
            Nationalities = new PersonNationalityDTO[0];
            Roles = new RoleDTO[0];
        }

        public PersonDTO(Person source)
        {
            Nationalities = source.PersonNationalities.OrderBy(x => x.CountryId).Select(x=>new PersonNationalityDTO(x)).ToArray();
            Roles = source.Roles.OrderBy(x => x.Id).Select(x => new RoleDTO(x)).ToArray();
            Age = source.Age;
            EducationInstitute = source.EducationInstitute;
            EducationSubject = source.EducationSubject.HasValue ? (int)source.EducationSubject : null;
            Gender = source.Gender.HasValue ? (int)source.Gender : null;
            HighEducation = source.HighEducation.HasValue ? (int)source.HighEducation : null;
            Kids = source.Kids;
            Id = source.Id;
            Married = source.Married;
            Name = source.Name;
            Race = source.Race.HasValue ? (int)source.Race : null; ;
            Religion = source.Religion.HasValue ? (int)source.Religion : null; ;
            Sexuality = source.Sexuality;
            Urban = source.Urban;
            VisibleDisability = source.VisibleDisability;
        }


        public PersonNationalityDTO[] Nationalities { get; set; }

        public RoleDTO[] Roles { get; set; }
        public short? Age { get; set; }
        public string EducationInstitute { get; set; }
        public int? EducationSubject { get; set; }
        public int? Gender { get; set; }
        public int? HighEducation { get; set; }
        public bool? Kids { get; set; }
        public int? Id { get; set; }
        public bool? Married { get; set; }
        public string Name { get; set; }
        public int? Race { get; set; }
        public int? Religion { get; set; }
        public string Sexuality { get; set; }
        public bool? Urban { get; set; }
        public string VisibleDisability { get; set; }
    }
}
