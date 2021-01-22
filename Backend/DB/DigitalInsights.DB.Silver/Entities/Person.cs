using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class Person
    {
        public Person()
        {
            PersonNationalities = new HashSet<PersonNationality>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string RandomName { get; set; }
        public short? Age { get; set; }
        public Common.Enums.Gender? Gender { get; set; }
        public Common.Enums.Race? Race { get; set; }
        public Common.Enums.Religion? Religion { get; set; }
        public bool? Married { get; set; }
        public bool? Kids { get; set; }
        public Common.Enums.EducationLevel? HighEducation { get; set; }
        public Common.Enums.EducationSubject? EducationSubject { get; set; }
        public string EducationInstitute { get; set; }
        public string Sexuality { get; set; }
        public string VisibleDisability { get; set; }
        public bool? Urban { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual ICollection<PersonNationality> PersonNationalities { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
