using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.Helpers
{
    static class EnumHelper
    {
        public static T? ParseInputEnumValue<T>(int? value) where T : struct
        {
            if (value.HasValue && Enum.IsDefined(typeof(T), value.Value))
            {
                throw new ArgumentException("Education Level not found");
            }

            return value.HasValue ? (T?)Enum.ToObject(typeof(T), value.Value) : null;
        }
    }
}
