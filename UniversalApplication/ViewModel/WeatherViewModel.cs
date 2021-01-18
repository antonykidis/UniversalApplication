using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //install Presentation framework.dll via Nuget
using UniversalApplication.Model;
using UniversalApplication.ViewModel.Helpers;
using UniversalApplication.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace UniversalApplication.ViewModel
{
    //implementing an interface
    public class WeatherViewModel : INotifyPropertyChanged
    {

        private string query;

        public string Query
        {
            get { return query; }
            set {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        //new property will hold the cities collection
        public ObservableCollection<City> Cities { get; set; }

        //slightly change from int to class
        private CurrentConditions currrentConditions;

        public CurrentConditions CurrrentConditions
        {
            get { return currrentConditions; }
            set {
                currrentConditions = value;
                OnPropertyChanged("CurrrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }
        public SearchCommand SearchCommand { get; set; }


        public WeatherViewModel()
        {
             //The first startup will be displaying Ney York ass a default
             //dummy location.
            bool istrue = true;

            if (istrue)
            {

                //initialize the values
                selectedCity = new City
                {
                    LocalizedName = "New York"
                };
                CurrrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = 21
                        }
                    }
                };

            }
            SearchCommand = new SearchCommand(this);
            istrue = false;
            Cities = new ObservableCollection<City>();
        }


        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrrentConditions = await AccueWeatherHelper.GetCurrentConditions(selectedCity.Key);
        }

        public async void MakeQuery()
        { 
            //get the cities by cliking the search button
            var cities = await AccueWeatherHelper.GetCities(Query);

            //clear the cities befor adding a new once
            // Attention! Clear IOBSERVABLECOLLECTON cities not the List<city>
            Cities.Clear();

            foreach (var city in cities)
            {
                //adding cities to IObservableCollection
                //It will apdate the view each time it is called
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Takes a Property name as a parameter.
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
