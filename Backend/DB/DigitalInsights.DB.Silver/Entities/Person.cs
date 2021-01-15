using DigitalInsights.DB.Common.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Person
    {
        public Person()
        {
            PersonCountries = new HashSet<PersonCountry>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short? Age { get; set; }
        public short? BirthYear { get; set; }
        public Gender? Gender { get; set; }
        public string Picture { get; set; }
        public Race? Race { get; set; }
        public Religion? Religion { get; set; }
        public MaritalStatus? Married { get; set; }
        public short? HasKids { get; set; }
        public Common.Enums.EducationLevel? EducationLevel { get; set; }
        public Common.Enums.EducationSubject? EducationSubject { get; set; }
        public string EducationInstitute { get; set; }
        public string Sexuality { get; set; }
        public string VisibleDisability { get; set; }
        public int? Urban { get; set; }
        public string Website { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual ICollection<PersonCountry> PersonCountries { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
