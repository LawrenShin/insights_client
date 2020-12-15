using CsvHelper;
using CsvHelper.Configuration;
using DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV.Auxiliary;
using DigitalInsights.DB.Lead.GLEIF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV
{
    class LegalAddressMap : ClassMap<LegalAddress>
    {
        public LegalAddressMap()
        {
            Map(m => m.Type).ConvertUsing((IReaderRow row) => "LEGAL");
            Map(m => m.Xmllang).Name("Entity.LegalAddress.xmllang");
            Map(m => m.Country).Name("Entity.LegalAddress.Country");
            Map(m => m.Region).Name("Entity.LegalAddress.Region");
            Map(m => m.City).Name("Entity.LegalAddress.City");
            Map(m => m.Postalcode).Name("Entity.LegalAddress.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.LegalAddress.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.LegalAddress.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.LegalAddress.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.LegalAddress.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.LegalAddress.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.LegalAddress.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.LegalAddress.AddressNumberWithinBuilding");
        }
    }

    class HeadquartersAddressMap : ClassMap<HeadquartersAddress>
    {
        public HeadquartersAddressMap()
        {
            Map(m => m.Type).ConvertUsing((IReaderRow row) => "HQ");
            Map(m => m.Xmllang).Name("Entity.HeadquartersAddress.xmllang");
            Map(m => m.Country).Name("Entity.HeadquartersAddress.Country");
            Map(m => m.Region).Name("Entity.HeadquartersAddress.Region");
            Map(m => m.City).Name("Entity.HeadquartersAddress.City");
            Map(m => m.Postalcode).Name("Entity.HeadquartersAddress.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.HeadquartersAddress.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.HeadquartersAddress.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.HeadquartersAddress.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.HeadquartersAddress.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.HeadquartersAddress.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.HeadquartersAddress.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.HeadquartersAddress.AddressNumberWithinBuilding");
        }
    }

    class OtherAddress1Map : ClassMap<OtherAddress1>
    {
        public OtherAddress1Map()
        {
            Map(m => m.Type).Name("Entity.OtherAddresses.OtherAddress.1.type");
            Map(m => m.Xmllang).Name("Entity.OtherAddresses.OtherAddress.1.xmllang");
            Map(m => m.Country).Name("Entity.OtherAddresses.OtherAddress.1.Country");
            Map(m => m.Region).Name("Entity.OtherAddresses.OtherAddress.1.Region");
            Map(m => m.City).Name("Entity.OtherAddresses.OtherAddress.1.City");
            Map(m => m.Postalcode).Name("Entity.OtherAddresses.OtherAddress.1.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.OtherAddresses.OtherAddress.1.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.OtherAddresses.OtherAddress.1.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.OtherAddresses.OtherAddress.1.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.OtherAddresses.OtherAddress.1.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.OtherAddresses.OtherAddress.1.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.OtherAddresses.OtherAddress.1.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.OtherAddresses.OtherAddress.1.AddressNumberWithinBuilding");
        }
    }

    class OtherAddress2Map : ClassMap<OtherAddress2>
    {
        public OtherAddress2Map()
        {
            Map(m => m.Type).Name("Entity.OtherAddresses.OtherAddress.2.type");
            Map(m => m.Xmllang).Name("Entity.OtherAddresses.OtherAddress.2.xmllang");
            Map(m => m.Country).Name("Entity.OtherAddresses.OtherAddress.2.Country");
            Map(m => m.Region).Name("Entity.OtherAddresses.OtherAddress.2.Region");
            Map(m => m.City).Name("Entity.OtherAddresses.OtherAddress.2.City");
            Map(m => m.Postalcode).Name("Entity.OtherAddresses.OtherAddress.2.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.OtherAddresses.OtherAddress.2.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.OtherAddresses.OtherAddress.2.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.OtherAddresses.OtherAddress.2.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.OtherAddresses.OtherAddress.2.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.OtherAddresses.OtherAddress.2.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.OtherAddresses.OtherAddress.2.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.OtherAddresses.OtherAddress.2.AddressNumberWithinBuilding");
        }
    }

    class OtherAddress3Map : ClassMap<OtherAddress3>
    {
        public OtherAddress3Map()
        {
            Map(m => m.Type).Name("Entity.OtherAddresses.OtherAddress.3.type");
            Map(m => m.Xmllang).Name("Entity.OtherAddresses.OtherAddress.3.xmllang");
            Map(m => m.Country).Name("Entity.OtherAddresses.OtherAddress.3.Country");
            Map(m => m.Region).Name("Entity.OtherAddresses.OtherAddress.3.Region");
            Map(m => m.City).Name("Entity.OtherAddresses.OtherAddress.3.City");
            Map(m => m.Postalcode).Name("Entity.OtherAddresses.OtherAddress.3.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.OtherAddresses.OtherAddress.3.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.OtherAddresses.OtherAddress.3.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.OtherAddresses.OtherAddress.3.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.OtherAddresses.OtherAddress.3.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.OtherAddresses.OtherAddress.3.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.OtherAddresses.OtherAddress.3.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.OtherAddresses.OtherAddress.3.AddressNumberWithinBuilding");
        }
    }

    class OtherAddress4Map : ClassMap<OtherAddress4>
    {
        public OtherAddress4Map()
        {
            Map(m => m.Type).Name("Entity.OtherAddresses.OtherAddress.4.type");
            Map(m => m.Xmllang).Name("Entity.OtherAddresses.OtherAddress.4.xmllang");
            Map(m => m.Country).Name("Entity.OtherAddresses.OtherAddress.4.Country");
            Map(m => m.Region).Name("Entity.OtherAddresses.OtherAddress.4.Region");
            Map(m => m.City).Name("Entity.OtherAddresses.OtherAddress.4.City");
            Map(m => m.Postalcode).Name("Entity.OtherAddresses.OtherAddress.4.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.OtherAddresses.OtherAddress.4.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.OtherAddresses.OtherAddress.4.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.OtherAddresses.OtherAddress.4.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.OtherAddresses.OtherAddress.4.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.OtherAddresses.OtherAddress.4.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.OtherAddresses.OtherAddress.4.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.OtherAddresses.OtherAddress.4.AddressNumberWithinBuilding");
        }
    }

    class OtherAddress5Map : ClassMap<OtherAddress5>
    {
        public OtherAddress5Map()
        {
            Map(m => m.Type).Name("Entity.OtherAddresses.OtherAddress.5.type");
            Map(m => m.Xmllang).Name("Entity.OtherAddresses.OtherAddress.5.xmllang");
            Map(m => m.Country).Name("Entity.OtherAddresses.OtherAddress.5.Country");
            Map(m => m.Region).Name("Entity.OtherAddresses.OtherAddress.5.Region");
            Map(m => m.City).Name("Entity.OtherAddresses.OtherAddress.5.City");
            Map(m => m.Postalcode).Name("Entity.OtherAddresses.OtherAddress.5.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.OtherAddresses.OtherAddress.5.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.OtherAddresses.OtherAddress.5.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.OtherAddresses.OtherAddress.5.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.OtherAddresses.OtherAddress.5.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.OtherAddresses.OtherAddress.5.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.OtherAddresses.OtherAddress.5.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.OtherAddresses.OtherAddress.5.AddressNumberWithinBuilding");
        }
    }

    class TransliteratedOtherAddress1Map : ClassMap<TransliteratedOtherAddress1>
    {
        public TransliteratedOtherAddress1Map()
        {
            Map(m => m.Type).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.type");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.xmllang");
            Map(m => m.Country).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.Country");
            Map(m => m.Region).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.Region");
            Map(m => m.City).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.City");
            Map(m => m.Postalcode).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.1.AddressNumberWithinBuilding");
        }
    }

    class TransliteratedOtherAddress2Map : ClassMap<TransliteratedOtherAddress2>
    {
        public TransliteratedOtherAddress2Map()
        {
            Map(m => m.Type).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.type");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.xmllang");
            Map(m => m.Country).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.Country");
            Map(m => m.Region).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.Region");
            Map(m => m.City).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.City");
            Map(m => m.Postalcode).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.2.AddressNumberWithinBuilding");
        }
    }

    class TransliteratedOtherAddress3Map : ClassMap<TransliteratedOtherAddress3>
    {
        public TransliteratedOtherAddress3Map()
        {
            Map(m => m.Type).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.type");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.xmllang");
            Map(m => m.Country).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.Country");
            Map(m => m.Region).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.Region");
            Map(m => m.City).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.City");
            Map(m => m.Postalcode).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.3.AddressNumberWithinBuilding");
        }
    }

    class TransliteratedOtherAddress4Map : ClassMap<TransliteratedOtherAddress4>
    {
        public TransliteratedOtherAddress4Map()
        {
            Map(m => m.Type).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.type");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.xmllang");
            Map(m => m.Country).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.Country");
            Map(m => m.Region).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.Region");
            Map(m => m.City).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.City");
            Map(m => m.Postalcode).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.4.AddressNumberWithinBuilding");
        }
    }

    class TransliteratedOtherAddress5Map : ClassMap<TransliteratedOtherAddress5>
    {
        public TransliteratedOtherAddress5Map()
        {
            Map(m => m.Type).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.type");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.xmllang");
            Map(m => m.Country).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.Country");
            Map(m => m.Region).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.Region");
            Map(m => m.City).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.City");
            Map(m => m.Postalcode).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.PostalCode");
            Map(m => m.Mailrouting).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.MailRouting");
            Map(m => m.Firstaddressline).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.FirstAddressLine");
            Map(m => m.Additionaladdressline1).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.AdditionalAddressLine.1");
            Map(m => m.Additionaladdressline2).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.AdditionalAddressLine.2");
            Map(m => m.Additionaladdressline3).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.AdditionalAddressLine.3");
            Map(m => m.Addressnumber).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.AddressNumber");
            Map(m => m.Addressnumberwithinbuilding).Name("Entity.TransliteratedOtherAddresses.TransliteratedOtherAddress.5.AddressNumberWithinBuilding");
        }
    }
}
