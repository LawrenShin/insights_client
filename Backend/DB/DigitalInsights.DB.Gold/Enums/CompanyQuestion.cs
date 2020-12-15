using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DB.Gold.Enums
{
    public enum CompanyQuestion
    {
        DIPolicyEstablished = 0, // Is there a Diversity & Inclusion Policy Established? 
        DIPolicyPublicallyAvailable = 1, // Is there a Diversity & Inclusion Policy Publically available? 
        DIPosition = 2, // Is there a Diversity & Inclusion Position? 
        DIPositionFullTimePosition = 3, // Is the Diversity & Inclusion Position, a Full Time Position? 
        DIOfficerExecutiveMember = 4, // Is the Diversity & Inclusion Officer an Executive member?
        DIHROrCompliance = 5, // Does D&I fall under HR or Compliance?
        DICodeofConduct = 6, // Is the Diversity & Inclusion included in Business Code of Conduct?
        DIManagingDiverse = 7, // Are managers trained in managing diverse employees?
        DIComplaintBox = 8, // Is there a D&I compliant box?
        DISupplyChain = 9, // Is Diversity & Inclusion incorporated into supply chain policies?
        DITalentGoals = 10, // Is D&I program measured by talent attraction and retention goals?
        DIEarnningCall = 11, // Was D&I mentioned within last 4 earning calls? (listed comp)
        NationalitySupportForLanguages = 12, // Does your company have initiatives in place to support different languages?
        NationalityResourceGroupForMultiCulture = 13, // Does the company have employee resource group for multi-culture?
        FamilyPaidParentalLeave = 14, // Is there a Paid parental leave?
        FamilyParentalLeavePolicyThreeMonths = 15, // Does the Company has a Parental Leave policy for a period more than 3 months? 
        FamilyPaternityLeaveSameAsMaternity = 16, // Is the Paternity Leave the same as the Maternity Leave, in terms of period and policy? 
        OrganizationalCompanyTownHalls = 17, // Does your company have company Town Halls?
        OrganizationalCompanyIntranet = 18, // Does your company have company Intranet?
        OrganizationalStructurePublished = 19, // Does your company publish the organizational structures?
        DisabilityWheelchairAccessible = 20, // Is offices accessible for wheelchair users?
        ReligionNonDiscriminatoryMeasures = 21, // Does your company have non-discriminatory meassures for religion in effect?
        ReligionInitiativesToSupport = 22, // Does your company have initiatives in place to support different Religions?
        ReligionSupportHolidays = 23, // Does the company Support/offer other Religions Holidays? 
        ReligionPrayerRoom = 24, // Does your company have a prayer room?
        SexualityAntiHarassmentPolicy = 25, // Does the company has an Anti-harrassment/discrimination policy in place?
        SexualitySupportOrientations = 26, // Does your company have initiatives in place to support different Sexual orientations?
        SexualityLBTQForum = 27, // Does your company have a forum for LGBTQ employees?
        SexualityOrientationData = 28, // Does your company have data on peoples sexual orientation?
        EducationContinuedEducationLeave = 29, // Does your company provide leave to support continued education?
        EducationFinancialEducationalSupport = 30, // Does your compay have financial educational support programs in place?
        EducationStudentDebtAssistance = 31, // Does your company have student debt assistance in place?
    }
}
