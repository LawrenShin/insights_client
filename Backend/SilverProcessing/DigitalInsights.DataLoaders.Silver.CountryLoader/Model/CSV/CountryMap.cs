using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Map(m => m.Name).Name("Country");
            Map(m => m.ISOCode).Name("ISOCode");
        }
    }
}
