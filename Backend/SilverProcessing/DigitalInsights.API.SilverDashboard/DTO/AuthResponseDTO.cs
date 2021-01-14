using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.API.SilverDashboard.DTO
{
    public class AuthResponseDTO
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
