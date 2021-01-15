using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities
{
    public partial class CompanyPublicData
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool LegalAddressEditable { get; set; }
        public int? LegalAddressId { get; set; }
        public bool HqAddressEditable { get; set; }
        public int? HqAddressId { get; set; }
        public int? NumEmployees { get; set; }
        public double? SeniorMgmtGenderRatioFemale { get; set; }
        public double? MiddleMgmtGenderRatioFemale { get; set; }
        public double? AllEmployeesGenderRatioFemale { get; set; }
        public double? GenderPayGapFemale { get; set; }
        public double? ExecutivesVisibleRaceMinority { get; set; }
        public double? BoardVisibleRaceMinority { get; set; }
        public double? MiddleMgmtVisibleRaceMinority { get; set; }
        public double? AllEmployeesVisibleRaceMinority { get; set; }
        public int? TotalHoursWorked { get; set; }
        public double? TotalTurnoverRate { get; set; }
        public double? VoluntaryTurnoverRate { get; set; }
        public double? InvoluntaryTurnoverRate { get; set; }
        public bool? DIOnWebsite { get; set; }
        public bool? CompanyOffersTraining { get; set; }
        public bool? CompanyHasSocialImpactPrograms { get; set; }
        public double? CompanySupplierSpendingWithDi { get; set; }
        public bool? CompanyHasProgramForAdvancingMinorities { get; set; }
        public bool? CompanyMeasuresEngagement { get; set; }
        public double? EngagementSurvey { get; set; }
        public double? EngagementSurveyResponseRate { get; set; }
        public int? Fatalities { get; set; }
        public double? SicknessAnsense { get; set; }
        public int? TotalRecordableInjuries { get; set; }
        public DateTime? EffectiveFrom { get; set; }

        public virtual Company Company { get; set; }
        public virtual Address HqAddress { get; set; }
        public virtual Address LegalAddress { get; set; }
    }
}
