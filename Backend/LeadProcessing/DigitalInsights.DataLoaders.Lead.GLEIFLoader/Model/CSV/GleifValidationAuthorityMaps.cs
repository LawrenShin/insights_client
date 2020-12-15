using CsvHelper.Configuration;
using DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV.Auxiliary;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV
{
    class ValidationAuthorityMap : ClassMap<ValidationAuthority>
    {
        public ValidationAuthorityMap()
        {
            Map(m => m.ValidationAuthorityId).Name("Registration.ValidationAuthority.ValidationAuthorityID");
            Map(m => m.ValidationAuthorityEntityId).Name("Registration.ValidationAuthority.ValidationAuthorityEntityID");
            Map(m => m.OtherValidationAuthorityId).Name("Registration.ValidationAuthority.OtherValidationAuthorityID");
            Map(m => m.Type).Constant("MAIN");
        }
    }

    class OtherValidationAuthority1Map : ClassMap<OtherValidationAuthority1>
    {
        public OtherValidationAuthority1Map()
        {
            Map(m => m.ValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.1.ValidationAuthorityID");
            Map(m => m.ValidationAuthorityEntityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.1.ValidationAuthorityEntityID");
            Map(m => m.OtherValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.1.OtherValidationAuthorityID");
            Map(m => m.Type).Constant("OTHER");
        }
    }

    class OtherValidationAuthority2Map : ClassMap<OtherValidationAuthority2>
    {
        public OtherValidationAuthority2Map()
        {
            Map(m => m.ValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.2.ValidationAuthorityID");
            Map(m => m.ValidationAuthorityEntityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.2.ValidationAuthorityEntityID");
            Map(m => m.OtherValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.2.OtherValidationAuthorityID");
            Map(m => m.Type).Constant("OTHER");
        }
    }

    class OtherValidationAuthority3Map : ClassMap<OtherValidationAuthority3>
    {
        public OtherValidationAuthority3Map()
        {
            Map(m => m.ValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.3.ValidationAuthorityID");
            Map(m => m.ValidationAuthorityEntityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.3.ValidationAuthorityEntityID");
            Map(m => m.OtherValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.3.OtherValidationAuthorityID");
            Map(m => m.Type).Constant("OTHER");
        }
    }

    class OtherValidationAuthority4Map : ClassMap<OtherValidationAuthority4>
    {
        public OtherValidationAuthority4Map()
        {
            Map(m => m.ValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.4.ValidationAuthorityID");
            Map(m => m.ValidationAuthorityEntityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.4.ValidationAuthorityEntityID");
            Map(m => m.OtherValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.4.OtherValidationAuthorityID");
            Map(m => m.Type).Constant("OTHER");
        }
    }

    class OtherValidationAuthority5Map : ClassMap<OtherValidationAuthority5>
    {
        public OtherValidationAuthority5Map()
        {
            Map(m => m.ValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.5.ValidationAuthorityID");
            Map(m => m.ValidationAuthorityEntityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.5.ValidationAuthorityEntityID");
            Map(m => m.OtherValidationAuthorityId).Name("Registration.OtherValidationAuthorities.OtherValidationAuthority.5.OtherValidationAuthorityID");
            Map(m => m.Type).Constant("OTHER");
        }
    }
}
