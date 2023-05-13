using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Helper
{
    public static class ApiHelper
    {
        //this helper class has generic methords for calling Rest api endpoints. (GET,POST,PUT and DELETE)
        public static IConfiguration Configuration { get; set; }
        public static HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Configuration["ApiBaseAddress"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        public static async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                T data;
                using (var client = CreateClient())
                {
                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        string d = await response.Content.ReadAsStringAsync();
                        if (d != null)
                        {
                            data = JsonConvert.DeserializeObject<T>(d);
                            return (T)data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Object o = new Object();
            return (T)o;
        }
        public static async Task<bool> PostRequestAsync<T>(string endpoint, T value)
        {
            using (var client = CreateClient())
            {
                var result = await client.PostAsJsonAsync("Freelancer", value);
                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }
        public static async Task<T> PostAsync<T>(string url, object param)
        {
            try
            {
                using (var client = CreateClient())
                {
                    T data;
                    string jsonData = JsonConvert.SerializeObject(param);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string d = await response.Content.ReadAsStringAsync();
                        if (d != null)
                        {
                            data = JsonConvert.DeserializeObject<T>(d);
                            return (T)data;
                        }
                    }
                }
                Object o = new Object();
                return (T)o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<T> PutAsync<T>(string url, object param)
        {
            using (var client = CreateClient())
            {
                T data;
                HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, contentPost);
                HttpContent content = response.Content;

                string d = await content.ReadAsStringAsync();
                if (d != null)
                {
                    data = JsonConvert.DeserializeObject<T>(d);
                    return (T)data;
                }
            }
            Object o = new Object();
            return (T)o;
        }

        public static async Task<T> DeleteAsync<T>(string url)
        {
            T newT;

            using (var client = CreateClient())
            {
                using (HttpResponseMessage response = await client.DeleteAsync(url))
                using (HttpContent content = response.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        newT = JsonConvert.DeserializeObject<T>(data);
                        return newT;
                    }
                }
            }
            Object o = new Object();
            return (T)o;
        }
    }
}
