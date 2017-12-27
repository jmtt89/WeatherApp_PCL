using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Load();
        }

        private async void Load()
        {
            using (var client = new HttpClient())
            {
                var geolocUrl = "http://ip-api.com/json";
                var jsonGeoloc = await client.GetStringAsync(geolocUrl);
                var geoObj = GeoIpMapProxy.FromJson(jsonGeoloc);

                var url = "http://api.openweathermap.org/data/2.5/weather?q=" + geoObj.City + "&APPID=8e44fd2ad82d53c04469e467010eb7b3";
                var jsonText = await client.GetStringAsync(url);

                var data = OpenWeatherMapProxy.FromJson(jsonText);
                Result.Text = "Nombre estacion: " + data.Name +
                    "\nTemperatura: " + data.Main.Temp +
                    "\nHumedad: " + data.Main.Humidity +
                    "\nDescripcion: " + data.Weather[0].Description;

                var iconUrl = "http://openweathermap.org/img/w/" + data.Weather[0].Icon + ".png";
                UriImageSource source = new UriImageSource();
                source.Uri = new Uri(iconUrl);
                Icon.Source = source;
            }
        }

        private async void GetInfo_Clicked(object sender, EventArgs e)
        {
            using(var client = new HttpClient())
            {
                var geolocUrl = "http://ip-api.com/json";
                var jsonGeoloc = await client.GetStringAsync(geolocUrl);
                var geoObj = GeoIpMapProxy.FromJson(jsonGeoloc);

                var url = "http://api.openweathermap.org/data/2.5/weather?q="+ City.Text +"&APPID=8e44fd2ad82d53c04469e467010eb7b3";
                var jsonText = await client.GetStringAsync(url);

                var data = OpenWeatherMapProxy.FromJson(jsonText);
                Result.Text = "Nombre estacion: " + data.Name +
                    "\nTemperatura: " + data.Main.Temp +
                    "\nHumedad: " + data.Main.Humidity +
                    "\nDescripcion: " + data.Weather[0].Description;

                var iconUrl = "http://openweathermap.org/img/w/" + data.Weather[0].Icon + ".png";
                UriImageSource source = new UriImageSource();
                source.Uri = new Uri(iconUrl);
                Icon.Source = source;
            }
        }
    }
}
