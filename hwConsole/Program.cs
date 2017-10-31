using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;

namespace hwConsole
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
            MessageAsync().Wait();
        }

         static async Task MessageAsync()
         {
            string UrlBase = ConfigurationSettings.AppSettings["APIBaseURL"];
            client.BaseAddress = new Uri(UrlBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get Message
                string results = await GetMessageAsync("hw/getmessage");

                // Print Message
                ShowMessage(results);

                // Wait
                Console.Read();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        static async Task<string> GetMessageAsync(string path)
        {
            string results = string.Empty;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
               results  = await response.Content.ReadAsStringAsync();
            }
            return results.Replace("\"", "");
        }

        static void ShowMessage(string message)
        {
            Console.WriteLine("The message is: " + message);
        }
    }
}
