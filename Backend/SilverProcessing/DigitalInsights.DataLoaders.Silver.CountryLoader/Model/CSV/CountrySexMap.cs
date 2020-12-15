using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountrySexMap : ClassMap<CountrySex>
    {
        public CountrySexMap()
        {
            Map(m => m.SameMarriage).Name("SameMarriage");
            Map(m => m.HomosexualTolerance).Name("HomoXTolerance");
            Map(m => m.HomosexualPop).Name("HomosexualPop");
            Map(m => m.SameAdopt).Name("LGBTAdopt");
        }
    }
}
