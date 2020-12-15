using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryEduMap : ClassMap<CountryEdu>
    {
        public CountryEduMap()
        {
            Map(m => m.ElementaryMale).Name("EduElementaryMale");
            Map(m => m.ElementaryFemale).Name("EduElementaryFemale");
            Map(m => m.ElementaryMf).Name("EduElementaryMF");
            Map(m => m.HighSchoolMale).Name("EduHighSchoolMale");
            Map(m => m.HighSchoolFemale).Name("EduHighSchoolFemale");
            Map(m => m.HighSchoolMf).Name("EduHighSchoolMF");
            Map(m => m.BachelorMale).Name("EduBachelorMale");
            Map(m => m.BachelorFemale).Name("EduBachelorFemale");
            Map(m => m.BachelorMf).Name("EduBachelorMF");
            Map(m => m.MasterMale).Name("EduMasterMale");
            Map(m => m.MasterFemale).Name("EduMasterFemale");
            Map(m => m.MasterMf).Name("EduMasterMF");
            Map(m => m.ExpectedEducation).Name("ExpectedEdu");
            Map(m => m.ActualEducation).Name("ActualEdu");
            Map(m => m.PublicFundingGdp).Name("EduPublicFundingGDP");
            Map(m => m.PublicFundFund).Name("EduPublicFundFund");
        }
    }
}
