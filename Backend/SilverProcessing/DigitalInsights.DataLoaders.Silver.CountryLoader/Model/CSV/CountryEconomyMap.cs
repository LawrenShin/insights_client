using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryEconomyMap : ClassMap<CountryEconomy>
    {
        public CountryEconomyMap()
        {
            Map(m => m.AvgIncome).Name("CountryAvgIncome");
            Map(m => m.Gdp).Name("CountryGDP");
            Map(m => m.GdpPerCapita).Name("CountryGDPCapita");
            Map(m => m.LabourForce).Name("CountryLaborForce");
            Map(m => m.GdpWorld).Name("CountryGDPWorld");
            Map(m => m.LabourForcePercent).Name("CountryLaborForce%");
            Map(m => m.MaleUnemploy).Name("CountryMaleUnemploy");
            Map(m => m.FemaleUnemploy).Name("CountryFemaleUnemploy");
            Map(m => m.Poor).Name("Poor");
            Map(m => m.EqualityLevel).Name("EqualityLevel");
        }
    }
}
