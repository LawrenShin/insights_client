using System;
using System.Collections.Generic;

namespace DigitalInsights.DB.Lead.GLEIF
{
    public partial class GleifValidationAuthority
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public string Type { get; set; }
        public string ValidationAuthorityId { get; set; }
        public string OtherValidationAuthorityId { get; set; }
        public string ValidationAuthorityEntityId { get; set; }

        public virtual GleifEntity Entity { get; set; }
    }
}
