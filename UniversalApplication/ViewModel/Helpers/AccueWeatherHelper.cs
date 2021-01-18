using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http; //http.Client
using UniversalApplication.Model;//model
using Newtonsoft.Json; //Deserialize Json

namespace UniversalApplication.ViewModel.Helpers
{

    //this class helps us to connect to the Accuweather API
    //use this format
    //GET "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=GlNEG8Vra7lo96KNJumGJ9CX6ZHUzG9u"
   
    
    public class AccueWeatherHelper
    {

        public const string BaseURL = "http://dataservice.accuweather.com/";
        public const string AutoCompleteEndPoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string currentConditionsEndPoint= "currentconditions/v1/{0}?apikey={1}";
        public const string APIKey = "GlNEG8Vra7lo96KNJumGJ9CX6ZHUzG9u";
        
        //return list of cities as a Task<> of type List Of Cities
        public static async Task <List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();
            //Callthe endpoint
            //first param is api-key. second param is- query.
            string url = BaseURL + string.Format(AutoCompleteEndPoint,APIKey, query);

            //Now make a request
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string Json = await response.Content.ReadAsStringAsync();
                //serialize json (add new packages from nuget Newtonsoft.Json)
                //Deserialize it as a List of cities.
                cities = JsonConvert.DeserializeObject<List<City>>(Json);

            }

            return cities;
   
        }

        //Return Current condition
        public static async Task<CurrentConditions> GetCurrentConditions(string citykey)
        {
            //creating an object
            CurrentConditions currentconditions = new CurrentConditions();
            //logic first param = city key. second param is api key
            string url = BaseURL + string.Format(currentConditionsEndPoint, citykey, APIKey);

            //Now make a request
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string Json = await response.Content.ReadAsStringAsync();
                //serialize json (add new packages from nuget Newtonsoft.Json)
                //Deserialize it as a List of cities.
                //Deserialize first item from a json array.
                currentconditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(Json)).FirstOrDefault();

            }

            return currentconditions;
        }
    }
}
