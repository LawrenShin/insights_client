using CsvHelper;
using CsvHelper.Configuration;
using DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV.Auxiliary;
using DigitalInsights.DB.Lead.GLEIF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Lead.GLEIFLoader.Model.CSV
{
    class LegalEntityNameMap : ClassMap<LegalEntityName>
    {
        public LegalEntityNameMap()
        {
            Map(m => m.Name).Name("Entity.LegalName");
            Map(m => m.Xmllang).Name("Entity.LegalName.xmllang");
            Map(m => m.Type).ConvertUsing((IReaderRow row) => "LEGAL");
        }
    }

    class OtherEntityName1Map : ClassMap<OtherEntityName1>
    {
        public OtherEntityName1Map()
        {
            Map(m => m.Name).Name("Entity.OtherEntityNames.OtherEntityName.1");
            Map(m => m.Xmllang).Name("Entity.OtherEntityNames.OtherEntityName.1.xmllang");
            Map(m => m.Type).Name("Entity.OtherEntityNames.OtherEntityName.1.type");
        }
    }

    class OtherEntityName2Map : ClassMap<OtherEntityName2>
    {
        public OtherEntityName2Map()
        {
            Map(m => m.Name).Name("Entity.OtherEntityNames.OtherEntityName.2");
            Map(m => m.Xmllang).Name("Entity.OtherEntityNames.OtherEntityName.2.xmllang");
            Map(m => m.Type).Name("Entity.OtherEntityNames.OtherEntityName.2.type");
        }
    }

    class OtherEntityName3Map : ClassMap<OtherEntityName3>
    {
        public OtherEntityName3Map()
        {
            Map(m => m.Name).Name("Entity.OtherEntityNames.OtherEntityName.3");
            Map(m => m.Xmllang).Name("Entity.OtherEntityNames.OtherEntityName.3.xmllang");
            Map(m => m.Type).Name("Entity.OtherEntityNames.OtherEntityName.3.type");
        }
    }

    class OtherEntityName4Map : ClassMap<OtherEntityName4>
    {
        public OtherEntityName4Map()
        {
            Map(m => m.Name).Name("Entity.OtherEntityNames.OtherEntityName.4");
            Map(m => m.Xmllang).Name("Entity.OtherEntityNames.OtherEntityName.4.xmllang");
            Map(m => m.Type).Name("Entity.OtherEntityNames.OtherEntityName.4.type");
        }
    }

    class OtherEntityName5Map : ClassMap<OtherEntityName5>
    {
        public OtherEntityName5Map()
        {
            Map(m => m.Name).Name("Entity.OtherEntityNames.OtherEntityName.5");
            Map(m => m.Xmllang).Name("Entity.OtherEntityNames.OtherEntityName.5.xmllang");
            Map(m => m.Type).Name("Entity.OtherEntityNames.OtherEntityName.5.type");
        }
    }

    class TransliteratedOtherEntityName1Map : ClassMap<TransliteratedOtherEntityName1>
    {
        public TransliteratedOtherEntityName1Map()
        {
            Map(m => m.Name).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.1");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.1.xmllang");
            Map(m => m.Type).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.1.type");
        }
    }

    class TransliteratedOtherEntityName2Map : ClassMap<TransliteratedOtherEntityName2>
    {
        public TransliteratedOtherEntityName2Map()
        {
            Map(m => m.Name).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.2");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.2.xmllang");
            Map(m => m.Type).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.2.type");
        }
    }

    class TransliteratedOtherEntityName3Map : ClassMap<TransliteratedOtherEntityName3>
    {
        public TransliteratedOtherEntityName3Map()
        {
            Map(m => m.Name).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.3");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.3.xmllang");
            Map(m => m.Type).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.3.type");
        }
    }

    class TransliteratedOtherEntityName4Map : ClassMap<TransliteratedOtherEntityName4>
    {
        public TransliteratedOtherEntityName4Map()
        {
            Map(m => m.Name).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.4");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.4.xmllang");
            Map(m => m.Type).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.4.type");
        }
    }

    class TransliteratedOtherEntityName5Map : ClassMap<TransliteratedOtherEntityName5>
    {
        public TransliteratedOtherEntityName5Map()
        {
            Map(m => m.Name).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.5");
            Map(m => m.Xmllang).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.5.xmllang");
            Map(m => m.Type).Name("Entity.TransliteratedOtherEntityNames.TransliteratedOtherEntityName.5.type");
        }
    }
}
