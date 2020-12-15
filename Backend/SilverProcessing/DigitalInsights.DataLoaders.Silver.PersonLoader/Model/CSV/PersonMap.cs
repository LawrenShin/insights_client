using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.PersonLoader.Model.CSV
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.Age).Name("Age");
            Map(m => m.BirthYear).Name("BirthYear");
            Map(m => m.Gender).Name("Gender");
            Map(m => m.Race).Name("Race");
            //Map(m => m.Nation).Name("Nation"); - done directly in transformer
            Map(m => m.HighEdu).Name("HighEdu");
            Map(m => m.EduSubject).Name("EduSubject");
            Map(m => m.EduInstitute).Name("EduInstitute");
            Map(m => m.Religion).Name("Religion");
            Map(m => m.Sexuality).Name("Sexuality");
            Map(m => m.Married).Name("Married");
        }
    }
}