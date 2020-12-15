using CsvHelper.Configuration;
using DigitalInsights.DB.Lead.GLEIF;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV
{
    class GleifEntityMap : ClassMap<GleifEntity>
    {
        public GleifEntityMap()
        {
            Map(m => m.Lei).Name("LEI");
            // Map(m => m.RegistrationAuthorityEntityId).Name("Entity.RegistrationAuthority.RegistrationAuthorityEntityID");
            Map(m => m.Category).Name("Entity.EntityCategory");
            // Map(m => m.EntityAssociatedEntityAssociatedEntityName).Name("Entity.AssociatedEntity.AssociatedEntityName");
            // Map(m => m.EntityAssociatedEntityAssociatedEntityNameXmlLang).Name("Entity.AssociatedEntity.AssociatedEntityName.xmllang");
            // Map(m => m.EntityAssociatedEntityAssociatedlei).Name("Entity.AssociatedEntity.AssociatedLEI");
            // Map(m => m.EntityAssociatedEntityType).Name("Entity.AssociatedEntity.type");
            Map(m => m.EntityExpirationReason).Name("Entity.EntityExpirationReason");
            Map(m => m.ExpirationDate).Name("Entity.EntityExpirationDate");
            Map(m => m.InitialRegistrationDate).Name("Registration.InitialRegistrationDate");
            Map(m => m.LastUpdateDate).Name("Registration.LastUpdateDate");
            Map(m => m.LegalFormCode).Name("Entity.LegalForm.EntityLegalFormCode");
            Map(m => m.LegalFormOtherLegalForm).Name("Entity.LegalForm.OtherLegalForm");
            Map(m => m.LegalJurisdiction).Name("Entity.LegalJurisdiction");
            Map(m => m.NextRenewalDate).Name("Registration.NextRenewalDate");
            // Map(m => m.OtherRegistrationAuthorityId).Name("Entity.RegistrationAuthority.RegistrationAuthorityID");
            // Map(m => m.RegistrationAuthorityId).Name("Entity.RegistrationAuthority.OtherRegistrationAuthorityID");
            // Map(m => m.RegistrationManagingLou).Name("Registration.ManagingLOU");
            Map(m => m.RegistrationStatus).Name("Registration.RegistrationStatus");
            // Map(m => m.RegistrationValidationSources).Name("Registration.ValidationSources");
            Map(m => m.Status).Name("Entity.EntityStatus");
            Map(m => m.SuccessorEntityName).Name("Entity.SuccessorEntity.SuccessorEntityName");
            Map(m => m.SuccessorEntityNameXmllang).Name("Entity.SuccessorEntity.SuccessorEntityName.xmllang");
            Map(m => m.SuccessorLei).Name("Entity.SuccessorEntity.SuccessorLEI");
        }
    }
}
