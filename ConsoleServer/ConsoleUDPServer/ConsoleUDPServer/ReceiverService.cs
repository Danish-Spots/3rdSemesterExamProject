using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using ConsoleUDPServer.Model;
using Newtonsoft.Json;

namespace ConsoleUDPServer
{
    
    public static class ReceiverService
    {

    
        public async static void StartService()
        {
            UdpClient client = new UdpClient();

            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 42069);
            client.Client.Bind(ip);


            //string message = Encoding.UTF8.GetString(client.Receive(ref ip));

            while (true)
            {
                string message = Encoding.UTF8.GetString(client.Receive(ref ip));
                Test t = JsonConvert.DeserializeObject<Test>(message);
                Console.WriteLine("\nRPI ID: " + t.Rpi_ID + "\nObj Temperature: " + t.Temperature + "\nHas Fever: " + t.HasFever);

                HttpResponseMessage m = await DataSenderService.Post("https://fevr.azurewebsites.net/api/Tests", t);
                try
                {
                    m.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("The application will now exit\n....");
                    break;
                }
                
            }
        }


    }
}
