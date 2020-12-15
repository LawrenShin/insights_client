using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities;

namespace DigitalInsights.DataLoaders.Silver.IndustryLoader.Model.CSV
{
    class IndustryMap : ClassMap<Industry>
    {
        public IndustryMap()
        {
            Map(m => m.Name).Name("Industry");
        }
    }
}
