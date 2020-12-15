using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities;

namespace DigitalInsights.DataLoaders.Silver.IndustryLoader.Model.CSV
{
    class IndustryCountryMap : ClassMap<IndustryCountry>
    {
        public IndustryCountryMap()
        {
            Map(m => m.AvgPay).Name("IndustryPay");
            Map(m => m.RententionRate).Name("IndustryRetention");
            Map(m => m.FlexibleHoursPledge).Name("IndustryPledgeFlex");
            Map(m => m.DiPledge).Name("IndustryPledgeDI");
            Map(m => m.HarassmentPledge).Name("IndustryHarassment");
            //Map(m => m.Age).Name("IndustryAge");
            //Map(m => m.Gender).Name("IndustryGender");
            Map(m => m.MaterintyLeavePledge).Name("IndustryPledgeMater");
            Map(m => m.PaternityLeavePledge).Name("IndustryPledgePater");
            Map(m => m.LgbtPledge).Name("IndustryPledgeLBGT");
            Map(m => m.AvgPay).Name("IndustryPledgeDisable");
            //Map(m => m.NonFatal).Name("IndustryNonFatal");
            //Map(m => m.Fatal).Name("IndustryFatal");
        }
    }
}
