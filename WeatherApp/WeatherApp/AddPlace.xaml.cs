
using System;
using System.IO;

using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlace : ContentPage
    {
        public ListView ListView;
        List<CityMapProxy> allCities = new List<CityMapProxy>();
        private Func<CityMapProxy, Boolean> Callback;

        public AddPlace(Func<CityMapProxy,Boolean> callback)
        {
            InitializeComponent();

            BindingContext = new CityViewModel();
            Load();
            ListView = CitiesResult;
            ListView.ItemSelected += ListView_ItemSelected;
            this.Callback = callback;
        }

        class CityViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CityMapProxy> Cities { get; set; }

            public CityViewModel()
            {
                Cities = new ObservableCollection<CityMapProxy>();
                /*
                MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                {
                    new MasterDetailPage1MenuItem { Id = 0, Title = "Page 1" },
                    new MasterDetailPage1MenuItem { Id = 1, Title = "Page 2" },
                    new MasterDetailPage1MenuItem { Id = 2, Title = "Page 3" },
                    new MasterDetailPage1MenuItem { Id = 3, Title = "Page 4" },
                    new MasterDetailPage1MenuItem { Id = 4, Title = "Page 5" },
                });
                */
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
            var assembly = typeof(AddPlace).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WeatherApp.city.list.json");
            using (var reader = new StreamReader(stream))
            {
                string json = await reader.ReadToEndAsync();
                allCities = CityMapProxy.FromJsonArray(json);
            }
        }

        private void SearchCity_SearchButtonPressed(object sender, EventArgs e)
        {

            var filtered = allCities.Where(FilterByName);

            var list = (BindingContext as CityViewModel).Cities;
            list.Clear();
            foreach (var city in filtered)
            {
                list.Add(city);
            }
            CitiesResult.IsVisible = true;
        }

        private bool FilterByName(CityMapProxy city)
        {
            return city.Name.ToLower().Contains(SearchCity.Text.ToLower());
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as CityMapProxy;
            if (item == null)
                return;
            Dissmiss(item);
            ListView.SelectedItem = null;
        }

        private async void Dissmiss(CityMapProxy city)
        {
            Callback(city);
            await Navigation.PopModalAsync();
        }

    }
}