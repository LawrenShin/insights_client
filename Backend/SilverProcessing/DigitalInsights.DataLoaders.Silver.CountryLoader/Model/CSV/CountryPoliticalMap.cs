using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryPoliticalMap : ClassMap<CountryPolitical>
    {
        public CountryPoliticalMap()
        {
            Map(m => m.Democracy).Name("DemocracyLevel");
            Map(m => m.Corruption).Name("CorruptionLevel");
            Map(m => m.FreedomSpeech).Name("FreedomSpeech");
        }
    }
}
