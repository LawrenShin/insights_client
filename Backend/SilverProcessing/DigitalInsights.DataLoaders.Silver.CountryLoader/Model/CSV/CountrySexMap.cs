using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountrySexualityMap : ClassMap<CountrySexuality>
    {
        public CountrySexualityMap()
        {
            Map(m => m.LGBTMarriage).Name("SameMarriage").ConvertUsing(x => x.GetField("SameMarriage") != "0.00");
            Map(m => m.LGBTTolerance).Name("HomoXTolerance");
            Map(m => m.HomosexualPopulation).Name("HomosexualPop");
            Map(m => m.LGBTAdoption).Name("LGBTAdopt").ConvertUsing(x => x.GetField("LGBTAdopt") != "0.00");
        }
    }
}
