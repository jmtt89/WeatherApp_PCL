using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace WeatherApp
{
    static class Serialize
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
        public static string ToJson(this GeoIpMapProxy self) => JsonConvert.SerializeObject(self, Settings);
        public static string ToJson(this OpenWeatherMapProxy self) => JsonConvert.SerializeObject(self, Settings);
        public static string ToJson(this CityMapProxy self) => JsonConvert.SerializeObject(self, Settings);
        public static string ToJson(this List<CityMapProxy> self) => JsonConvert.SerializeObject(self, Settings);
    }
}
