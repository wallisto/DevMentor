using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:2269/api/") };

            PrintContacts(client);

            Console.WriteLine("press enter to exit");
            Console.ReadLine();

        }

        private async static void PrintContacts(HttpClient client)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("contacts");

            string responseContent = await responseMessage.Content.ReadAsStringAsync();

            dynamic contacts = JArray.Parse(responseContent);

            foreach (dynamic contact in contacts)
            {
                Console.WriteLine("{0} : {1}", (string)contact.Name, (string)contact.Email);
            }
                
        }
    }
}
