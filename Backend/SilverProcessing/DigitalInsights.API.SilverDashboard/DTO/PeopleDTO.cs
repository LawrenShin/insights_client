using DigitalInsights.DB.Silver.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class PeopleDTO
    {
        public PeopleDTO()
        {
            People = new PersonDTO[0];
            Pagination = new PaginationDTO();
        }

        public PeopleDTO(
            Person[] people,
            int pageSize,
            int pageIndex,
            int pageCount)
        {
            People = people.Select(x => new PersonDTO(x)).ToArray();
            Pagination = new PaginationDTO()
            {
                PageCount = pageCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        [JsonProperty("people")]
        public PersonDTO[] People { get; set; }

        [JsonProperty("pagination")]
        public PaginationDTO Pagination { get; set; }
    }
}
