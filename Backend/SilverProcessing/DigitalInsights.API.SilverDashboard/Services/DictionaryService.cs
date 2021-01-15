﻿using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.DB.Common.Enums;
using DigitalInsights.DB.Silver;
using DigitalInsights.DB.Silver.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Services
{
    internal class DictionaryService
    {
        SilverContext silverContext;

        public DictionaryService(SilverContext context)
        {
            silverContext = context;
        }

        public CountryDTO[] GetCountries()
        {
            return silverContext.Countries.Select(x=>new CountryDTO() { Id = x.Id, Name = x.Name, ISOCode = x.Code}).ToArray();
        }

        public EnumDTO[] GetEducationLevels()
        {
            return silverContext.EducationLevels.Select(x => new EnumDTO { Id = x.Id, Name = x.Name }).ToArray();
        }

        public EnumDTO[] GetEducationSubjects()
        {
            return silverContext.EducationSubjects.Select(x => new EnumDTO { Id = x.Id, Name = x.Name }).ToArray();
        }

        public EnumDTO[] GetGenders()
        {
            return Enum.GetValues<Gender>().Select(x => new EnumDTO { Id = (int)x, Name = x.ToString() }).ToArray();
        }

        public EnumDTO[] GetIndustryCodes()
        {
            return Enum.GetValues<IndustryCode>().Select(x => new EnumDTO { Id = (int)x, Name = x.ToString() }).ToArray();
        }

        public EnumDTO[] GetIndustries()
        {
            return silverContext.Industries.Select(x => new EnumDTO { Id = x.Id, Name = x.Name }).ToArray();
        }

        public EnumDTO[] GetMaritalStatuses()
        {
            return Enum.GetValues<MaritalStatus>().Select(x => new EnumDTO { Id = (int)x, Name = x.ToString() }).ToArray();
        }

        public EnumDTO[] GetRaces()
        {
            return Enum.GetValues<Race>().Select(x => new EnumDTO { Id = (int)x, Name = x.ToString() }).ToArray();
        }

        public EnumDTO[] GetRegions()
        {
            return silverContext.Regions.Select(x => new EnumDTO { Id = x.Id, Name = x.Name }).ToArray();
        }

        public EnumDTO[] GetReligions()
        {
            return Enum.GetValues<Religion>().Select(x => new EnumDTO { Id = (int)x, Name = x.ToString() }).ToArray();
        }

        public EnumDTO[] GetRoleTypes()
        {
            return Enum.GetValues<RoleType>().Select(x => new EnumDTO { Id = (int)x, Name = x.ToString() }).ToArray();
        }
    }
}
