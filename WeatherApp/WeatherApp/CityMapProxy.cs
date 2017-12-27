using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class CityMapProxy
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }
    }

    public partial class CityMapProxy
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
        public static List<CityMapProxy> FromJsonArray(string json) => JsonConvert.DeserializeObject<List<CityMapProxy>>(json, Settings);
        public static CityMapProxy FromJson(string json) => JsonConvert.DeserializeObject<CityMapProxy>(json, Settings);
    }


}
