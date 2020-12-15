using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryAgeMap : ClassMap<CountryAge>
    {
        public CountryAgeMap()
        {
            Map(m => m.Dis19).Name("AgeDis19");
            Map(m => m.Dis39).Name("AgeDis39");
            Map(m => m.Dis59).Name("AgeDis59");
            Map(m => m.Dis70).Name("AgeDis79");
            Map(m => m.Disx).Name("AgeDisX");
            Map(m => m.Avg18).Name("AgeAv18");
            Map(m => m.Parliament).Name("AgeParliament");
            Map(m => m.Ministers).Name("AgeMinisters");
        }
    }
}
