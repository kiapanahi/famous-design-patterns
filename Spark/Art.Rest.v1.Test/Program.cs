using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Art.Rest.v1.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string endpoint = "http://localhost:58125/api/v1/products";

            Console.WriteLine("Please wait until website is fully loaded...");
            Console.WriteLine("Then hit ENTER to start...");
            Console.ReadLine();

            // Get products with artist information

            var products = Get<List<ClientProduct>>(endpoint + "?expand=artist").Result;
            Console.WriteLine("Read " + products.Count + " products with Artist details");

            var product = Get<ClientProduct>(endpoint + "/4").Result;
            Console.WriteLine("Read single product with title: " + product.Title);

            // Insert new product

            product = new ClientProduct
            {
                Title = "Test title",
                Description = "Great art work",
                Price = 100,
                Image = "art.jpg",
                AvgStars = 0,
                QuantitySold = 0,
                Artist = new ClientArtist { Href = product.Href }
            };

            product = Post<ClientProduct>(endpoint, product).Result;
            Console.WriteLine("Inserted product with href: " + product.Href);

            // Update product 

            product.Title = "Another title";
            product.Price = 400;
            product = Put<ClientProduct>(product.Href, product).Result;
            Console.WriteLine("Changed product with href: " + product.Href);

            // Delete product

            product = Delete<ClientProduct>(product.Href).Result;
            Console.WriteLine("Deleted product with href: " + product.Href);
            Console.WriteLine();

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
