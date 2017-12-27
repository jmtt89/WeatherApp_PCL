using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail : ContentPage
    {
        public MasterDetailPage1Detail()
        {
            InitializeComponent();
        }

        public void SetCity(CityMapProxy city)
        {
            Load(city);
        }

        private async void Load(CityMapProxy city)
        {
            using (var client = new HttpClient())
            {
                var url = "http://api.openweathermap.org/data/2.5/weather?id=" + city.Id + "&APPID=8e44fd2ad82d53c04469e467010eb7b3&units=metric";
                var jsonText = await client.GetStringAsync(url);

                var data = OpenWeatherMapProxy.FromJson(jsonText);

                Description.Text = data.Weather[0].Description;
                Temperature.Text = data.Main.Temp + "°C";



                //LastUpdate.Text = String.Format("{0:G}", (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(data.Dt));
                TemperatureMin.Text = "Min: " + data.Main.TempMin + " °C";
                TemperatureMax.Text = "Max: " + data.Main.TempMax + " °C";
                Temperature.Text = data.Main.Temp + " °C";
                Pressure.Text = data.Main.Pressure + " Pascales";
                Humidity.Text = data.Main.Humidity + " %";


                //Sunrise.Text = String.Format("{0:T}", (new DateTime(1970, 1, 1)).AddSeconds(data.Sys.Sunrise));
                //Sunset.Text = String.Format("{0:T}", (new DateTime(1970, 1, 1)).AddSeconds(data.Sys.Sunset));

                
                var iconUrl = "http://openweathermap.org/img/w/" + data.Weather[0].Icon + ".png";
                UriImageSource source = new UriImageSource();
                source.Uri = new Uri(iconUrl);
                Logo.Source = source;
                

                //Logo.Source = ImageSource.FromResource("WeatherApp.Assets.WeatherIcons." + data.Weather[0].Icon + ".png");
            }
        }
    }
}