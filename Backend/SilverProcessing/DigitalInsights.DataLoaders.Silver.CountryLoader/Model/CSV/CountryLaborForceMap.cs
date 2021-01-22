using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;

namespace DigitalInsights.DataLoaders.Silver.CountryLoader.Model.CSV
{
    class CountryLaborForceMap : ClassMap<CountryLaborForce>
    {
        public CountryLaborForceMap()
        {
            Map(m => m.AverageIncome).Name("CountryAvgIncome");
            Map(m => m.LaborForce).Name("CountryLaborForce");
            Map(m => m.LaborForcePercentage).Name("CountryLaborForce%");
            Map(m => m.MaleUnemployment).Name("CountryMaleUnemploy");
            Map(m => m.FemaleUnemployment).Name("CountryFemaleUnemploy");
        }
    }


}
