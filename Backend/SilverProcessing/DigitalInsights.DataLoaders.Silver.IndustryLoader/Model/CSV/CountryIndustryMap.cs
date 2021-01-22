using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities.CountryData;

namespace DigitalInsights.DataLoaders.Silver.IndustryLoader.Model.CSV
{
    class CountryIndustryMap : ClassMap<CountryIndustry>
    {
        public CountryIndustryMap()
        {
            Map(m => m.AveragePay).Name("IndustryPay");
            Map(m => m.Retention).Name("IndustryRetention");
            Map(m => m.FlexibleHours).Name("IndustryPledgeFlex");
            //Map(m => m.D).Name("IndustryPledgeDI");
            Map(m => m.Harassment).Name("IndustryHarassment");
            Map(m => m.Age).Name("IndustryAge");
            Map(m => m.Gender).Name("IndustryGender");
            Map(m => m.Maternity).Name("IndustryPledgeMater");
            Map(m => m.Paternity).Name("IndustryPledgePater");
            Map(m => m.LBGT).Name("IndustryPledgeLBGT");
            Map(m => m.Disabled).Name("IndustryPledgeDisable");
            Map(m => m.InjuriesNonFatal).Name("IndustryNonFatal");
            Map(m => m.InjuriesFatal).Name("IndustryFatal");
        }
    }
}
