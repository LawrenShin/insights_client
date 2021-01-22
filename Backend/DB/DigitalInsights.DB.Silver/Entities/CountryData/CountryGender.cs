using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountryGender
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public double MalePopulationPercentage { get; set; }
        public double FemalePopulationpercetage { get; set; }
        public double FemaleWorkforce { get; set; }
        public double FemaleWorkforcePercentage { get; set; }
        public double FemaleWorkforcePopulationPercentage { get; set; }
        public double GenderWorkGap { get; set; }
        public double GenderHealthGap { get; set; }
        public double GenderEducationcationGap { get; set; }
        public double GenderPoliticalGap { get; set; }
        public double FemalePromotionPolicy { get; set; }
        public double WomenEducation { get; set; }
        public double Maternity { get; set; }
        public double Paternity { get; set; }
        public double IncomeGap { get; set; }
        public double WomenViolence { get; set; }
        public double FemaleParliamentShare { get; set; }
        public double FemaleMinisterShare { get; set; }
        public double LifeExpectancyMale { get; set; }
        public double LifeExpectancyFemale { get; set; }
        public double MaleSuicide { get; set; }
        public double FemaleSuicide { get; set; }
        public double EducatedMaleUnemploy { get; set; }
        public double EducatedFemaleUnemploy { get; set; }
        public double FirmsFemaleOwnership { get; set; }
        public double FirmsFemaleManager { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
