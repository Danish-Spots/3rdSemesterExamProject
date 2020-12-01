using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleUDPServer.Model;
using Newtonsoft.Json;

namespace ConsoleUDPServer
{
    class DataSenderService
    {
    
        public static async Task<HttpResponseMessage> Post(string url, Test newT)
        {
            using (HttpClient client = new HttpClient())
            {
                string ObjectJson = JsonConvert.SerializeObject(newT);
                var data = new StringContent(ObjectJson, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage message = await client.PostAsync(url, data);
                    message.EnsureSuccessStatusCode();
                    return message;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }
                
                
            }
        }




    }

}
