using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryUrbanMap : ClassMap<CountryUrban>
    {
        public CountryUrbanMap()
        {
            Map(m => m.CitiesPop).Name("LiveCities");
        }
    }
}