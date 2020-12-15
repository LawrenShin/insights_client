using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryEdu
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public double ElementaryMf { get; set; }
        public double ElementaryMale { get; set; }
        public double ElementaryFemale { get; set; }
        public double HighSchoolMf { get; set; }
        public double HighSchoolMale { get; set; }
        public double HighSchoolFemale { get; set; }
        public double BachelorMf { get; set; }
        public double BachelorMale { get; set; }
        public double BachelorFemale { get; set; }
        public double MasterMf { get; set; }
        public double MasterMale { get; set; }
        public double MasterFemale { get; set; }
        public double TotalMf { get; set; }
        public double ExpectedEducation { get; set; }
        public double ActualEducation { get; set; }
        public double PublicFundingGdp { get; set; }
        public double PublicFundFund { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
