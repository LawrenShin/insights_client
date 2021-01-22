using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryEconomicPowerMap : ClassMap<CountryEconomicPower>
    {
        public CountryEconomicPowerMap()
        {
            Map(m => m.GDP).Name("CountryGDP");
            Map(m => m.GDPPerCapita).Name("CountryGDPCapita");
            Map(m => m.GDPWorld).Name("CountryGDPWorld");
        }
    }
}
