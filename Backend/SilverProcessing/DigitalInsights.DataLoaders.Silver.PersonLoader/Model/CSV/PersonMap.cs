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
            Map(m => m.Gender).Name("Gender").ConvertUsing(x =>
            {
                string gender = x.GetField("Gender").Trim().ToLower();
                switch (gender)
                {
                    case "female": return DB.Common.Enums.Gender.Female;
                    case "male": return DB.Common.Enums.Gender.Male;
                    default: return null;
                }
            });
            Map(m => m.Race).Name("Race").ConvertUsing(x=>
            {
                string race = x.GetField("Race").Trim().ToLower();
                switch(race)
                {
                    case "caucasian": return DB.Common.Enums.Race.Caucasian;
                    case "arab": return DB.Common.Enums.Race.Arab;
                    case "asian": return DB.Common.Enums.Race.Asian;
                    case "black": return DB.Common.Enums.Race.Black;
                    case "hispanic": return DB.Common.Enums.Race.Hispanic;
                    case "indigenous": return DB.Common.Enums.Race.Indigenous;
                    default: return null;
                }
            });
            //Map(m => m.Nation).Name("Nation"); - done directly in transformer
            Map(m => m.EducationLevel).Name("HighEdu").ConvertUsing(x =>
            {
                string educationLevel = x.GetField("HighEdu").Trim().ToLower();
                switch (educationLevel)
                {
                    case "bachelor": return DB.Common.Enums.EducationLevel.Bachelor;
                    case "elementary": return DB.Common.Enums.EducationLevel.Elementary;
                    case "high school": return DB.Common.Enums.EducationLevel.HighSchool;
                    case "master": return DB.Common.Enums.EducationLevel.Master;
                    case "master+": 
                    case "master plus": 
                        return DB.Common.Enums.EducationLevel.MasterPlus;
                    default: return null;
                }
            });
            Map(m => m.EducationSubject).Name("EduSubject").ConvertUsing(x =>
            {
                string educationSubject = x.GetField("EduSubject").Trim().ToLower();
                switch (educationSubject)
                {

                    case "agriculture": return DB.Common.Enums.EducationSubject.Agriculture;
                    case "archaeology & ethnology": return DB.Common.Enums.EducationSubject.ArchaeologyAndEthnology;
                    case "architecture": return DB.Common.Enums.EducationSubject.Architecture;
                    case "art & literature": return DB.Common.Enums.EducationSubject.ArtAndLiterature;
                    case "astronomy": return DB.Common.Enums.EducationSubject.Astronomy;
                    case "bioengineering": return DB.Common.Enums.EducationSubject.Bioengineering;
                    case "biology": return DB.Common.Enums.EducationSubject.Biology;
                    case "business/business administration": return DB.Common.Enums.EducationSubject.BusinessOrBusinessAdministration;
                    case "chemistry": return DB.Common.Enums.EducationSubject.Chemistry;
                    case "communications, journalism & writing": return DB.Common.Enums.EducationSubject.CommunicationsJournalismAndWriting;
                    case "computer science/information systems": return DB.Common.Enums.EducationSubject.ComputerScienceOrInformationSystems;
                    case "economics": return DB.Common.Enums.EducationSubject.Economics;
                    case "education": return DB.Common.Enums.EducationSubject.Education;
                    case "engineering": return DB.Common.Enums.EducationSubject.Engineering;
                    case "environmental science": return DB.Common.Enums.EducationSubject.EnvironmentalScience;
                    case "ergonomics": return DB.Common.Enums.EducationSubject.Ergonomics;
                    case "finance & accounting": return DB.Common.Enums.EducationSubject.FinanceAndAccounting;
                    case "forestry": return DB.Common.Enums.EducationSubject.Forestry;
                    case "geography": return DB.Common.Enums.EducationSubject.Geography;
                    case "history": return DB.Common.Enums.EducationSubject.History;
                    case "human resources": return DB.Common.Enums.EducationSubject.HumanitiesEthnicAndCulturalStudies;
                    case "humanities, ethnic and cultural studies": return DB.Common.Enums.EducationSubject.HumanResources;
                    case "law": return DB.Common.Enums.EducationSubject.Law;
                    case "library and information science": return DB.Common.Enums.EducationSubject.LibraryAndInformationScience;
                    case "linguistics": return DB.Common.Enums.EducationSubject.Linguistics;
                    case "logistics and supply chain": return DB.Common.Enums.EducationSubject.LogisticsAndSupplyChain;
                    case "management": return DB.Common.Enums.EducationSubject.Management;
                    case "manufacturing": return DB.Common.Enums.EducationSubject.Manufacturing;
                    case "mathematics": return DB.Common.Enums.EducationSubject.Mathematics;
                    case "medicine & health sciences": return DB.Common.Enums.EducationSubject.MedicineAndHealthSciences;
                    case "military": return DB.Common.Enums.EducationSubject.Military;
                    case "music, film & dance": return DB.Common.Enums.EducationSubject.MusiFilmAndDance;
                    case "philosophy": return DB.Common.Enums.EducationSubject.Philosophy;
                    case "physics": return DB.Common.Enums.EducationSubject.Physics;
                    case "political science": return DB.Common.Enums.EducationSubject.PoliticalScience;
                    case "psychology": return DB.Common.Enums.EducationSubject.Psychology;
                    case "public affairs": return DB.Common.Enums.EducationSubject.PublicAffairs;
                    case "religious studies": return DB.Common.Enums.EducationSubject.ReligiousStudies;
                    case "sales & marketing": return DB.Common.Enums.EducationSubject.SalesAndMarketing;
                    case "sociology": return DB.Common.Enums.EducationSubject.Sociology;
                    default: return null;
                }
            }); 
            Map(m => m.EducationInstitute).Name("EduInstitute");
            Map(m => m.Religion).Name("Religion").ConvertUsing(x =>
            {
                string religion = x.GetField("Religion").Trim().ToLower();
                switch (religion)
                {
                    case "buddhism": return DB.Common.Enums.Religion.Buddhism;
                    case "christian": return DB.Common.Enums.Religion.Christian;
                    case "hindu": return DB.Common.Enums.Religion.Hindu;
                    case "judaism": return DB.Common.Enums.Religion.Judaism;
                    case "muslim": return DB.Common.Enums.Religion.Muslim;
                    case "other": return DB.Common.Enums.Religion.Other;
                    default: return null;
                }
            });
            Map(m => m.Sexuality).Name("Sexuality");
            Map(m => m.Married).Name("Married").ConvertUsing(x =>
            {
                string maritalStatus = x.GetField("Married").Trim().ToLower();
                switch (maritalStatus)
                {
                    case "yes": return DB.Common.Enums.MaritalStatus.Yes;
                    case "no": return DB.Common.Enums.MaritalStatus.No;
                    default: return null;
                }
            });
        }
    }
}