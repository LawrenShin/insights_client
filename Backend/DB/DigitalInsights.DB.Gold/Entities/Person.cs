using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class Person
    {
        public Person()
        {
            PersonCountries = new HashSet<PersonCountry>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public int? Address { get; set; }
        public string Name { get; set; }
        public short? Age { get; set; }
        public short? BirthYear { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
        public string Race { get; set; }
        public string Religion { get; set; }
        public string Married { get; set; }
        public string HighEdu { get; set; }
        public string EduSubject { get; set; }
        public string EduInstitute { get; set; }
        public string Sexuality { get; set; }
        public string Disability { get; set; }
        public double? BaseSalary { get; set; }
        public double? OtherIncentive { get; set; }
        public bool? Urban { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Address AddressNavigation { get; set; }
        public virtual ICollection<PersonCountry> PersonCountries { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
