using System;
using System.Collections.Generic;
using System.Text;
using ConsoleUDPServer.Model;

namespace ConsoleUDPServer
{
    public static class DataSorterService
    {

        public static Test SortData(string message)
        {
            Test newTest = new Test();
            double temp = Convert.ToDouble(message.Split(";")[0]);
            int Rip_ID = Convert.ToInt32(message.Split(";")[1]);

            newTest.Temperature = temp;
            newTest.Rpi_ID = Rip_ID;
            newTest.HasFever = CheckIfHasFever(temp);
            newTest.TimeOfDataRec = DateTime.Now;

            return newTest;
        }

        private static bool CheckIfHasFever(double temp)
        {
            if (temp > 37.5)
            {
                return true;
            }

            return false;
        }

    }
}
