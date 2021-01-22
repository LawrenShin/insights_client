using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryGenderMap : ClassMap<CountryGender>
    {
        public CountryGenderMap()
        {
            Map(m => m.MalePopulationPercentage).Name("MalePop%");
            Map(m => m.FemalePopulationpercetage).Name("FemalePop%");
            Map(m => m.WomenEducation).Name("WomenEdu");
            Map(m => m.FemaleWorkforce).Name("FemaleWorkForce");
            Map(m => m.FemaleWorkforcePercentage).Name("FemaleWorkForcePercentage");
            Map(m => m.Maternity).Name("Materinty");
            Map(m => m.Paternity).Name("Paternity");
            Map(m => m.GenderWorkGap).Name("GenderWorkGap");
            Map(m => m.GenderHealthGap).Name("GenderHealthGap");
            Map(m => m.GenderEducationcationGap).Name("GenderEduGap");
            Map(m => m.GenderPoliticalGap).Name("GenderPolGap");
            Map(m => m.IncomeGap).Name("IncomeGap");
            Map(m => m.WomenViolence).Name("Womenviolence");
            Map(m => m.FemaleParliamentShare).Name("ParliamentShare");
            Map(m => m.FemaleMinisterShare).Name("MinisterShare");
            Map(m => m.FemalePromotionPolicy).Name("FemalePromotionPolicy");
            Map(m => m.LifeExpectancyMale).Name("LifeMale");
            Map(m => m.LifeExpectancyFemale).Name("LifeFemale");
        }
    }
}
