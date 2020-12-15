using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryDemographicsMap : ClassMap<CountryDemographics>
    {
        public CountryDemographicsMap()
        {
            Map(m => m.Population).Name("CountryPopulation");
            Map(m => m.ImmigrantPop).Name("CountryImmigrantPop");
            Map(m => m.ImmigrantPercent).Name("CountryImmigrantPercent");
        }
    }
}
