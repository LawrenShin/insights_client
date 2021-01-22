using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryUrbanizationMap : ClassMap<CountryUrbanization>
    {
        public CountryUrbanizationMap()
        {
            Map(m => m.LiveCities).Name("LiveCities");
        }
    }
}