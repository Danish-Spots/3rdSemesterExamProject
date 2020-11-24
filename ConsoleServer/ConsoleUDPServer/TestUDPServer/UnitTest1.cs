using System;
using System.Net.Http.Headers;
using ConsoleUDPServer;
using ConsoleUDPServer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUDPServer
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MeasurementModelTest()
        {
            //Arrange
            Test test = new Test();
            DateTime timeNow;

            //Act
            timeNow = DateTime.Now;
            test.ID = 1;
            test.Temperature = 36.7;
            test.TimeOfDataRec = timeNow;
            test.Rpi_ID = 13;
            test.HasFever = false;

            //Assert
            Assert.AreEqual(1 , test.ID);
            Assert.AreEqual(36.7, test.Temperature);
            Assert.AreEqual(timeNow, test.TimeOfDataRec);
            Assert.AreEqual(13,test.Rpi_ID);
            Assert.AreEqual(false,test.HasFever);
        }


        [TestMethod]
        public void SortDataTest()
        {
            //Arrange
            DateTime timeNow = DateTime.Now;
            Test expectedTest = new Test(35.7,0,1,false,timeNow);
            Test actualTest = new Test();
           
            
            //Act
            actualTest = DataSorterService.SortData("35.7;1");
            actualTest.TimeOfDataRec = timeNow;
            

            //Assert
            Assert.AreEqual(expectedTest.ID, actualTest.ID);
            Assert.AreEqual(expectedTest.Temperature, actualTest.Temperature);
            Assert.AreEqual(expectedTest.TimeOfDataRec, actualTest.TimeOfDataRec);
            Assert.AreEqual(expectedTest.Rpi_ID, actualTest.Rpi_ID);
            Assert.AreEqual(expectedTest.HasFever, actualTest.HasFever);

        }

        }
}
