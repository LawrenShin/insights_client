using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities.CompanyData
{
    public partial class CompanyDIMetrics
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool? HarassmentPolicy { get; set; }
        public bool? SocialProgram { get; set; }
        public bool? Retaliation { get; set; }
        public double? SupplySpend { get; set; }
        public double? ValueDISupplySpend { get; set; }
        public double? DISupplySpendRevenueRatio { get; set; }
        public bool? MentorProgram { get; set; }
        public bool? SocialEvents { get; set; }
        public bool? EmployEngagement { get; set; }
        public double? EmploySatisfactionSurvey { get; set; }
        public double? EmploySurveyResponseRate { get; set; }
        public bool? DIPolicyEstablished { get; set; }
        public bool? DIPublicAvailable { get; set; }
        public bool? DIWebsite { get; set; }
        public bool? DIPosition { get; set; }
        public bool? DIFTEPosition { get; set; }
        public bool? DIPositionExecutive { get; set; }
        public bool? DIDivision { get; set; }
        public bool? DICodeConduct { get; set; }
        public bool? ManagingDiverse { get; set; }
        public bool? DIComplaint { get; set; }
        public bool? DISupplyChain { get; set; }
        public bool? DITalentGoals { get; set; }
        public bool? DIEarningCall { get; set; }
        public string HolidaySupport { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
    }
}
