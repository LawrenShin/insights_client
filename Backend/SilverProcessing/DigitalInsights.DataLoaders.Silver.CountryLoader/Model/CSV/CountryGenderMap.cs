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
            Map(m => m.MalePop).Name("MalePop%");
            Map(m => m.FemalePop).Name("FemalePop%");
            Map(m => m.WomenEdu).Name("WomenEdu");
            Map(m => m.FemaleWorkForce).Name("FemaleWorkForce");
            Map(m => m.FemaleWorkForcePercent).Name("FemaleWorkForcePercentage");
            Map(m => m.MaterintyLeave).Name("Materinty");
            Map(m => m.PaternityLeave).Name("Paternity");
            Map(m => m.GenderWorkGap).Name("GenderWorkGap");
            Map(m => m.GenderHealthGap).Name("GenderHealthGap");
            Map(m => m.GenderEduGap).Name("GenderEduGap");
            Map(m => m.GenderPolGap).Name("GenderPolGap");
            Map(m => m.IncomeGap).Name("IncomeGap");
            Map(m => m.WomenViolence).Name("Womenviolence");
            Map(m => m.FemaleParliamentShare).Name("ParliamentShare");
            Map(m => m.FemaleMinisterShare).Name("MinisterShare");
            Map(m => m.FemalePromotionPolicy).Name("FemalePromotionPolicy");
            Map(m => m.LifeMale).Name("LifeMale");
            Map(m => m.LifeFemale).Name("LifeFemale");

        }
    }
}
