﻿using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryReligionMap : ClassMap<CountryReligion>
    {
        public CountryReligionMap()
        {
            Map(m => m.Muslim).Name("ReligionMuslim");
            Map(m => m.Christian).Name("ReligionChristian");
            Map(m => m.Hindu).Name("ReligionHindu");
            Map(m => m.Buddishm).Name("ReligionBuddishm");
            Map(m => m.Judaism).Name("ReligionJudaism");
            Map(m => m.Other).Name("ReligionOther");
            Map(m => m.StateReligion).Name("StateReligion").ConvertUsing(x => x.GetField("StateReligion") != "0.00");
            Map(m => m.ReligionFreedom).Name("ReligionFreedom").ConvertUsing(x => x.GetField("ReligionFreedom") != "0.00");
        }
    }
}
