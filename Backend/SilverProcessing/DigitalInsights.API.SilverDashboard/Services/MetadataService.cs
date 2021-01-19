using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Services
{
    class MetadataService
    {
        private SilverContext silverContext;

        public MetadataService(SilverContext silverContext)
        {
            this.silverContext = silverContext;
        }

        internal EntityMetadataDTO[] GetUIMetadata()
        {
            return new EntityMetadataDTO[]
            {
                new EntityMetadataDTO()
                {
                    EntityName = "company",
                    PropertyMetadata = new PropertyMetadataDTO[]
                    {
                        new PropertyMetadataDTO()
                        {
                            PropertyName = "id",
                            FieldType = FieldType.String.ToString(),
                            IsEditable = false,
                        },
                        new PropertyMetadataDTO()
                        {
                            PropertyName = "name",
                            DisplayName = "Company name",
                            Description = "A name of a company",
                            FieldType = FieldType.String.ToString(),
                            IsEditable = true,
                            AllowsNull = false
                        },
                        new PropertyMetadataDTO()
                        {
                            PropertyName = "lei",
                            DisplayName = "LEI",
                            Description = "LEI identifier according to GLEIF database",
                            FieldType = FieldType.String.ToString(),
                            IsEditable = true,
                            AllowsNull = false
                        },
                        new PropertyMetadataDTO()
                        {
                            PropertyName = "numberOfEmployees",
                            DisplayName = "Number of employees",
                            FieldType = FieldType.Integer.ToString(),
                            RangeLow = "0",
                            IsEditable = true,
                            AllowsNull = true
                        },
                        new PropertyMetadataDTO()
                        {
                            PropertyName = "totalTurnoverRate",
                            DisplayName = "Total turnover rate",
                            FieldType = FieldType.Percentage.ToString(),
                            RangeLow = "0",
                            RangeHigh = "100",
                            IsEditable = true,
                            AllowsNull = true
                        },
                        new PropertyMetadataDTO()
                        {
                            PropertyName = "companyHasProgramForAdvancingMinorities",
                            DisplayName = "Does company have a program for advancing minorities?",
                            FieldType = FieldType.Boolean.ToString(),
                            IsEditable = true,
                            AllowsNull = true
                        },
                        new PropertyMetadataDTO()
                        {
                            PropertyName = "companyIndustries",
                            DisplayName = "Industries",
                            FieldType = FieldType.Array.ToString(),
                            EntityMetadata = new EntityMetadataDTO()
                            {
                                EntityName = "companyIndustry",
                                PropertyMetadata = new PropertyMetadataDTO[]
                                {
                                    new PropertyMetadataDTO()
                                    {
                                        PropertyName = "industry",
                                        DisplayName = "Industry",
                                        Description = "Industrial category of a company",
                                        FieldType = FieldType.DropDown.ToString(),
                                        Dictionary = "industries",
                                        IsEditable = true,
                                        AllowsNull = false
                                    },
                                    new PropertyMetadataDTO()
                                    {
                                        PropertyName = "industryCodes",
                                        DisplayName = "Industry code",
                                        Description = "Industrial code according to which organization",
                                        FieldType = FieldType.DropDown.ToString(),
                                        Dictionary = "industryCodes",
                                        IsEditable = true,
                                        AllowsNull = true
                                    },
                                }
                            },
                            IsEditable = true,
                            AllowsNull = false,
                        },
                    }
                }
            };
        }
    }
}
