using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.DB.Silver.Enums
{
    [Flags]
    public enum RoleType
    {
        Executive = 1,
        Board = 2,
        Both = 3,
    }
}
