using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Master : ContentPage
    {
        public ListView ListView;

        public MasterDetailPage1Master()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPage1MasterViewModel();
            Load();
            ListView = MenuItemsListView;
        }

        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CityMapProxy> MenuItems { get; set; }

            public MasterDetailPage1MasterViewModel()
            {
                MenuItems = new ObservableCollection<CityMapProxy>();
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private async void Load()
        {
            using (var client = new HttpClient())
            {
                var geolocUrl = "http://ip-api.com/json";
                var jsonGeoloc = await client.GetStringAsync(geolocUrl);
                var geoObj = GeoIpMapProxy.FromJson(jsonGeoloc);

                var url = "http://api.openweathermap.org/data/2.5/weather?lat="+geoObj.Lat+"&lon="+geoObj.Lon+"&APPID=8e44fd2ad82d53c04469e467010eb7b3&units=metric";
                var jsonText = await client.GetStringAsync(url);

                var data = OpenWeatherMapProxy.FromJson(jsonText);

                var tmp = BindingContext as MasterDetailPage1MasterViewModel;
                tmp.MenuItems.Add(new CityMapProxy
                {
                    Id = (int)data.Id,
                    Name = data.Name,
                    Country = data.Sys.Country,
                    Coord = data.Coord
                });

                Location.Text = data.Name;
                //Description.Text = data.Weather[0].Description;
                Temperature.Text = data.Main.Temp + "°C";

                /*
                var iconUrl = "http://openweathermap.org/img/w/" + data.Weather[0].Icon + ".png";
                UriImageSource source = new UriImageSource();
                source.Uri = new Uri(iconUrl);
                Icon.Source = source;
                */

                Icon.Source = ImageSource.FromResource("WeatherApp.Assets.WeatherIcons." + data.Weather[0].Icon + ".png");

            }
        }

        private async Task Add_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddPlace(AddNewPlace));
        }

        private Boolean AddNewPlace(CityMapProxy city)
        {
            var tmp = BindingContext as MasterDetailPage1MasterViewModel;
            tmp.MenuItems.Add(city);
            return true;
        }
    }
}