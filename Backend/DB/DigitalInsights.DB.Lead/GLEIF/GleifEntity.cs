using DigitalInsights.DB.Lead.GLEIF;
using System;
using System.Collections.Generic;

namespace DigitalInsights.DB.Lead.GLEIF
{
    public partial class GleifEntity
    {
        public GleifEntity()
        {
            GleifAddresses = new HashSet<GleifAddress>();
            GleifEntityNames = new HashSet<GleifEntityName>();
            //GleifValidationAuthorities = new HashSet<GleifValidationAuthority>();
        }

        public long Id { get; set; }
        public string Lei { get; set; }
        /*public string RegistrationAuthorityId { get; set; }
        public string OtherRegistrationAuthorityId { get; set; }
        public string RegistrationAuthorityEntityId { get; set; }*/
        public string LegalJurisdiction { get; set; }
        public string Category { get; set; }
        public string LegalFormCode { get; set; }
        public string LegalFormOtherLegalForm { get; set; }
        /*public string EntityAssociatedEntityType { get; set; }
        public string EntityAssociatedEntityAssociatedlei { get; set; }
        public string EntityAssociatedEntityAssociatedEntityName { get; set; }
        public string EntityAssociatedEntityAssociatedEntityNameXmlLang { get; set; }*/
        public string Status { get; set; }
        public string ExpirationDate { get; set; }
        public string EntityExpirationReason { get; set; }
        public string SuccessorLei { get; set; }
        public string SuccessorEntityName { get; set; }
        public string SuccessorEntityNameXmllang { get; set; }
        public string InitialRegistrationDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string RegistrationStatus { get; set; }
        public string NextRenewalDate { get; set; }
        /*public string RegistrationManagingLou { get; set; }
        public string RegistrationValidationSources { get; set; }*/

        public virtual ICollection<GleifAddress> GleifAddresses { get; set; }
        public virtual ICollection<GleifEntityName> GleifEntityNames { get; set; }
        //public virtual ICollection<GleifValidationAuthority> GleifValidationAuthorities { get; set; }
    }
}
