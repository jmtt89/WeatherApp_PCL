using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class GeoIpMapProxy
    {
        [JsonProperty("as")]
        public string As { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("isp")]
        public string Isp { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("regionName")]
        public string RegionName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }

    public partial class GeoIpMapProxy
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
        public static GeoIpMapProxy FromJson(string json) => JsonConvert.DeserializeObject<GeoIpMapProxy>(json, Settings);
    }
}
