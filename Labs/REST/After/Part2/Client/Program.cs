using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:2269/api/") };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            PrintContacts(client);

            AddContact(client, "Bob Smith", "bob@smith.com", "Imaginary Friend");

            Console.WriteLine("press enter to exit");
            Console.ReadLine();

        }

        private async static void AddContact(HttpClient client, string name, string email, string notes)
        {
            dynamic contact = new JObject();
            contact.Name = name;
            contact.Email = email;
            contact.Notes = notes;

            var content = new StringContent(contact.ToString());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync("contacts", content);

            Console.WriteLine(response.StatusCode);
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
