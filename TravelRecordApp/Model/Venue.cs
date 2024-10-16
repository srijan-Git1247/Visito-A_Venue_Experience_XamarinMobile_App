using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{

    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }


    public class DropOff
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Main
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Roof
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Geocodes
    {
        public DropOff drop_off { get; set; }
        public Main main { get; set; }
        public Roof roof { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string formatted_address { get; set; }
        public string locality { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
    }


    public class Child
    {
        public string fsq_id { get; set; }
        public IList<Category> categories { get; set; }
        public string name { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public string plural_name { get; set; }
        public Icon icon { get; set; }
    }

    public class Parent
    {
        public string fsq_id { get; set; }
        public IList<Category> categories { get; set; }
        public string name { get; set; }
    }

    public class RelatedPlaces
    {
        public IList<Child> children { get; set; }
        public Parent parent { get; set; }
    }

    public class Result//Venue
    {
        public string fsq_id { get; set; }
        public IList<Category> categories { get; set; }
     
       
        public Location location { get; set; }
        public string name { get; set; }


        //Added code MVVM
        public async static Task<List<Result>> GetVenues(double latitiude, double longitude)
        {
            var venues = new List<Result>();
            var url = VenueRoot.GenerateURL(latitiude, longitude);

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Add("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Authorization", Constants.API_KEY);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = venueRoot.results as List<Result>;
            }

            return venues;
        }

    }

    public class Center
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Circle
    {
        public Center center { get; set; }
        public int radius { get; set; }
    }

    public class GeoBounds
    {
        public Circle circle { get; set; }
    }

    public class Context
    {
        public GeoBounds geo_bounds { get; set; }
    }

    public class Example
    {
        public IList<Result> results { get; set; }
        public Context context { get; set; }
    }

    public class VenueRoot
    {
        public IList<Result> results { get; set; }
        public Context context { get; set; }
        public static string GenerateURL(double latitude, double longitude)
        {
            return string.Format(Constants.VENUE_SEARCH, latitude, longitude);
        }
    }
}
