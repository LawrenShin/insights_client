﻿using System;

#nullable disable

namespace DigitalInsights.DB.Gold.Entities
{
    public partial class PersonNationality
    {
        public int PersonNationalityId { get; set; }
        public int PersonId { get; set; }
        public int CountryId { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
        public virtual Person Person { get; set; }
    }
}
