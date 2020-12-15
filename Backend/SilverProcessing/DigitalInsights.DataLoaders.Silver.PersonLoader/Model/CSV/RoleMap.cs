using CsvHelper.Configuration;
using DigitalInsights.DB.Silver.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalInsights.DataLoaders.Silver.PersonLoader.Model.CSV
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Map(m => m.RoleType).Name("Role");
            Map(m => m.Title).Name("Title");
        }
    }
}
