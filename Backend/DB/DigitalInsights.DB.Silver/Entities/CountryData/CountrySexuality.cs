using System;

#nullable disable

namespace DigitalInsights.DB.Silver.Entities.CountryData
{
    public partial class CountrySexuality
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public bool LGBTMarriage { get; set; }
        public double LGBTTolerance { get; set; }
        public double HomosexualPopulation { get; set; }
        public bool LGBTAdoption { get; set; }
        public double TransgenderRights { get; set; }
        public double ConversionTherapy { get; set; }
        public double LGBTMarketing { get; set; }
        public double LGBTAntiLaws { get; set; }
        public double LGBTDeathSentences { get; set; }
        public double LGBTMurders { get; set; }
        public double LGBTDiscriminationLaw { get; set; }
        public DateTime EffectiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
