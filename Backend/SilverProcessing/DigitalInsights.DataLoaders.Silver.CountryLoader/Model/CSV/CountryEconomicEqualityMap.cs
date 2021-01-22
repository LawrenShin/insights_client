using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryEconomicEqualityMap : ClassMap<CountryEconomicEquality>
    {
        public CountryEconomicEqualityMap()
        {

            Map(m => m.Poor).Name("Poor");
            Map(m => m.EqualityIndex).Name("EqualityLevel");
        }
    }


}
