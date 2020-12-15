using System;
using System.Collections.Generic;

namespace DigitalInsights.DB.Lead.GLEIF
{
    public partial class GleifEntityName
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public string Name { get; set; }
        public string Xmllang { get; set; }
        public string Type { get; set; }

        public virtual GleifEntity Entity { get; set; }
    }
}
