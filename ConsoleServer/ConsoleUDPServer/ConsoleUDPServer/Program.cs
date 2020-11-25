using System;
using System.Net;
using System.Net.Sockets;

namespace ConsoleUDPServer
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Console server is up and running.");
         
            ReceiverService.StartService();


        }



    }
}
