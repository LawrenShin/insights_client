using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryRaceMap : ClassMap<CountryRace>
    {
        public CountryRaceMap()
        {
            Map(m => m.Black).Name("RaceBlack");
            Map(m => m.Asian).Name("RaceAsian");
            Map(m => m.Hispanic).Name("RaceHispanic");
            Map(m => m.Arab).Name("RaceArab");
            Map(m => m.Caucasian).Name("RaceCaucasian");
            Map(m => m.Indegineous).Name("RaceIndegineous");
            Map(m => m.DiscriminationLaw).Name("RaceDiscriminationLaw");
        }
    }
}
