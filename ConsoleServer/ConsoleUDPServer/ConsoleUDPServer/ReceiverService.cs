using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleUDPServer.Model;

namespace ConsoleUDPServer
{
    
    public static class ReceiverService
    {

         static ReceiverService()
        {
        }

        public static void StartService()
        {
            UdpClient client = new UdpClient();

            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 42069);
            client.Client.Bind(ip);


            string message = Encoding.ASCII.GetString(client.Receive(ref ip));

            while (true)
            {
                message = Encoding.ASCII.GetString(client.Receive(ref ip));
                Test test = DataSorterService.SortData(message);
                //Has to be finished
                DataSenderService.Post("", test);
            }
        }


    }
}
