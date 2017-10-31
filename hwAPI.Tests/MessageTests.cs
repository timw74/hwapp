using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hwAPI;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;

namespace hwAPI.Tests
{
    [TestClass]
    public class MessageTests
    {

        static HttpClient client = new HttpClient();

        [TestMethod]
        public void APITest()
        {
            MessageAsyncTest().Wait();
        }

        public async Task MessageAsyncTest()
        {
            string UrlBase = ConfigurationSettings.AppSettings["APIBaseURL"];
            client.BaseAddress = new Uri(UrlBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Get Message
            string results = await GetMessageAsync("hw/getmessage");

            Assert.AreEqual("Hello World", results);
        }

        static async Task<string> GetMessageAsync(string path)
        {
            string results = string.Empty;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                results = await response.Content.ReadAsStringAsync();
            }
            return results.Replace("\"", "");
        }
    }
}
