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

        public bool? DICodeConduct { get; private set; }
        public bool? DIComplaint { get; private set; }
        public bool? DIDivision { get; private set; }
        public bool? DIEarningCall { get; private set; }
        public bool? DIFTEPosition { get; private set; }
        public bool? DIPolicyEstablished { get; private set; }
        public bool? DIPosition { get; private set; }
        public bool? DIPositionExecutive { get; private set; }
        public bool? DIPublicAvailable { get; private set; }
        public bool? DISupplyChain { get; private set; }
        public double? DISupplySpendRevenueRatio { get; private set; }
        public bool? DITalentGoals { get; private set; }
        public bool? DIWebsite { get; private set; }
        public bool? EmployEngagement { get; private set; }
        public double? EmploySatisfactionSurvey { get; private set; }
        public double? EmploySurveyResponseRate { get; private set; }
        public bool? HarassmentPolicy { get; private set; }
        public string HolidaySupport { get; private set; }
        public bool? ManagingDiverse { get; private set; }
        public bool? MentorProgram { get; private set; }
        public bool? Retaliation { get; private set; }
        public bool? SocialEvents { get; private set; }
        public bool? SocialProgram { get; private set; }
        public double? SupplySpend { get; private set; }
        public double? ValueDISupplySpend { get; private set; }
    }
}
