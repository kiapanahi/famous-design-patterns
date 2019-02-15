using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Rest.v1.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string endpoint = "http://localhost:52112/api/v1/users";

            Console.WriteLine("Please wait until website is fully loaded...");
            Console.WriteLine("Then hit ENTER to start...");
            Console.ReadLine();

            // Get users with task information

            var users = Get<List<ClientUser>>(endpoint + "?expand=task").Result;
            Console.WriteLine("Read " + users.Count + " users with Task details");
            
            //..

            Console.Write("Hit ENTER to quit...");
            Console.ReadKey();
        }

        #region async helper methods

        // SELECT

        static async Task<T> Get<T>(string endpoint)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);
                string content = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
        }

        // INSERT

        static async Task<T> Post<T>(string endpoint, object data)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(data));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(endpoint, httpContent);
                string content = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
        }

        // UPDATE

        static async Task<T> Put<T>(string endpoint, object data)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(data));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(endpoint, httpContent);
                string content = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
        }

        // DELETE

        static async Task<T> Delete<T>(string endpoint)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(endpoint);
                string content = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
        }

        #endregion
    }
}
