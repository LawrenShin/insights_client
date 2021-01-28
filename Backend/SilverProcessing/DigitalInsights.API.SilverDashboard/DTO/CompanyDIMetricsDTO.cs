using DigitalInsights.DB.Silver.Entities.CompanyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompanyDIMetricsDTO
    {
        public CompanyDIMetricsDTO()
        {

        }
        public CompanyDIMetricsDTO(CompanyDIMetrics source)
        {
            DICodeConduct = source.DICodeConduct;
            DIComplaint = source.DIComplaint;
            DIDivision = source.DIDivision;
            DIEarningCall = source.DIEarningCall;
            DIFTEPosition = source.DIFTEPosition;
            DIPolicyEstablished = source.DIPolicyEstablished;
            DIPosition = source.DIPosition;
            DIPositionExecutive = source.DIPositionExecutive;
            DIPublicAvailable = source.DIPublicAvailable;
            DISupplyChain = source.DISupplyChain;
            DISupplySpendRevenueRatio = source.DISupplySpendRevenueRatio;
            DITalentGoals = source.DITalentGoals;
            DIWebsite =source.DIWebsite;
            EmployEngagement = source.EmployEngagement;
            EmploySatisfactionSurvey = source.EmploySatisfactionSurvey;
            EmploySurveyResponseRate = source.EmploySurveyResponseRate;
            HarassmentPolicy = source.HarassmentPolicy;
            HolidaySupport = source.HolidaySupport;
            ManagingDiverse = source.ManagingDiverse;
            MentorProgram = source.MentorProgram;
            Retaliation = source.Retaliation;
            SocialEvents = source.SocialEvents;
            SocialProgram = source.SocialProgram;
            SupplySpend = source.SupplySpend;
            ValueDISupplySpend = source.ValueDISupplySpend;
        }

        public bool? DICodeConduct { get; set; }
        public bool? DIComplaint { get; set; }
        public bool? DIDivision { get; set; }
        public bool? DIEarningCall { get; set; }
        public bool? DIFTEPosition { get; set; }
        public bool? DIPolicyEstablished { get; set; }
        public bool? DIPosition { get; set; }
        public bool? DIPositionExecutive { get; set; }
        public bool? DIPublicAvailable { get; set; }
        public bool? DISupplyChain { get; set; }
        public double? DISupplySpendRevenueRatio { get; set; }
        public bool? DITalentGoals { get; set; }
        public bool? DIWebsite { get; set; }
        public bool? EmployEngagement { get; set; }
        public double? EmploySatisfactionSurvey { get; set; }
        public double? EmploySurveyResponseRate { get; set; }
        public bool? HarassmentPolicy { get; set; }
        public string HolidaySupport { get; set; }
        public bool? ManagingDiverse { get; set; }
        public bool? MentorProgram { get; set; }
        public bool? Retaliation { get; set; }
        public bool? SocialEvents { get; set; }
        public bool? SocialProgram { get; set; }
        public double? SupplySpend { get; set; }
        public double? ValueDISupplySpend { get; set; }
    }
}
