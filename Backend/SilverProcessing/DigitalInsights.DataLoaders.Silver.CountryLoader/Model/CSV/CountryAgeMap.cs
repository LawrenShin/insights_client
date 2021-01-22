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
            Map(m => m.AgeDistribution19).Name("AgeDis19");
            Map(m => m.AgeDistribution39).Name("AgeDis39");
            Map(m => m.AgeDistribution59).Name("AgeDis59");
            Map(m => m.AgeDistribution79).Name("AgeDis79");
            Map(m => m.AgeDistributionx).Name("AgeDisX");
            Map(m => m.AgeAverage18).Name("AgeAv18");
            Map(m => m.AgeParliament).Name("AgeParliament");
            Map(m => m.AgeMinisters).Name("AgeMinisters");
        }
    }
}
