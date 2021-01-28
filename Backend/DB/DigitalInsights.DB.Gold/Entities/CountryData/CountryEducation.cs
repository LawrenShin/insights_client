using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CountryData
{
    public partial class CountryEducation
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double ElementaryMaleFemale { get; set; }
        public double ElementaryMale { get; set; }
        public double ElementaryFemale { get; set; }
        public double HighSchoolMaleFemale { get; set; }
        public double HighSchoolMale { get; set; }
        public double HighSchoolFemale { get; set; }
        public double BachelorMaleFemale { get; set; }
        public double BachelorMale { get; set; }
        public double BachelorFemale { get; set; }
        public double MasterMaleFemale { get; set; }
        public double MasterMale { get; set; }
        public double MasterFemale { get; set; }
        public double DoctoralMaleFemale { get; set; }
        public double DoctoralMale { get; set; }
        public double DoctoralFemale { get; set; }
        public double ExpectedEducation { get; set; }
        public double ActualEducation { get; set; }
        public double EducationPublicFundingGDP { get; set; }
        public double EducationPublicFundFund { get; set; }
        public double MaleLiteracy { get; set; }
        public double FemaleLiteracy { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
