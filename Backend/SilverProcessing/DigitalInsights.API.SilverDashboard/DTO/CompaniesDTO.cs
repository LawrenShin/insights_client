﻿using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class CompaniesDTO
    {
        public CompaniesDTO()
        {
            Companies = new CompanyDTO[0];
            Pagination = new PaginationDTO();
        }

        public CompaniesDTO(
            Company[] company,
            int pageSize,
            int pageIndex,
            int pageCount)
        {
            Companies = company.Select(x => new CompanyDTO(x)).ToArray();
            Pagination = new PaginationDTO()
            {
                PageCount = pageCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        [JsonProperty("companies")]
        public CompanyDTO[] Companies { get; set; }

        [JsonProperty("pagination")]
        public PaginationDTO Pagination { get; set; }
    }
}