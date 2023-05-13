//using GoogleGson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkHiveMobileApp.ViewModel;

namespace WorkHiveMobileApp.Services
{
    //This class is used for making rest api calls to Web api
    public class RestService 
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<Jobs> Items { get; private set; }
        private string apiURL = "https://apiworkhive.azurewebsites.net/api/Jobs/";
        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Jobs>> GetJobList()
        {
             Items = new List<Jobs>();
            string content = "";
            Uri uri = new Uri(apiURL);
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                     content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject(content);
                    Items =   JsonConvert.DeserializeObject<List<Jobs>>(obj.ToString());
                }
            }
            catch (Exception ex)
            {
               
            }
            return Items;
        }
        public async Task<Jobs> GetJobDetails(int id)
        {

            Jobs ob = new Jobs();
            string content = "";
            Uri uri = new Uri(apiURL+ "/GetDetails/" + id);
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject(content);                  
                    ob = JsonConvert.DeserializeObject<Jobs>(obj.ToString());
                }
            }
            catch (Exception ex)
            {
            }
            return ob;
        }
    }
}
