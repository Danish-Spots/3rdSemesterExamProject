using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;

namespace ApiUnitTest
{
    [TestClass]
    public class ModelsTest
    {
        [TestMethod]
        public void TestProfile()
        {
            //Arrange
            Profile profile = new Profile();
            DateTime date = DateTime.Now;
            //Act
            profile.ID = 0;
            profile.CompanyName = "testCN";
            profile.City = "testCi";
            profile.JoinDate = date;
            profile.Phone = "2139";
            profile.Address = "testAd";
            profile.Country = "testCo";
            //Assert
            Assert.AreEqual(profile.ID, 0);
            Assert.AreEqual(profile.CompanyName, "testCN");
            Assert.AreEqual(profile.City, "testCi");
            Assert.AreEqual(profile.JoinDate, date);
            Assert.AreEqual(profile.Phone, "2139");
            Assert.AreEqual(profile.Address, "testAd");
            Assert.AreEqual(profile.Country, "testCo");
        }

        [TestMethod]
        public void TestUser()
        {
            //Arrange
            User u = new User();

            //Act
            u.ID = 1;
            u.Email = "testemail";
            u.UserName = "testusername";
            u.Password = "testpassword";
            u.ProfileID = 1;

            //Assert
            Assert.AreEqual(u.ID, 1);
            Assert.AreEqual(u.Email, "testemail");
            Assert.AreEqual(u.UserName, "testusername");
            Assert.AreEqual(u.Password, "testpassword");
            Assert.AreEqual(u.ProfileID, 1);
        }

        [TestMethod]
        public void TestRaspberryPi()
        {
            //Arrange
            RaspberryPi pi = new RaspberryPi();

            //Act
            pi.ID = 1;
            pi.Location = "test location";
            pi.IsActive = true;
            pi.ProfileID = 1;

            //Assert
            Assert.AreEqual(pi.ID, 1);
            Assert.AreEqual(pi.Location, "test location");
            Assert.AreEqual(pi.IsActive, true);
            Assert.AreEqual(pi.ProfileID, 1);
        }

        [TestMethod]
        public void TestTests()
        {
            //Arrange
            Test t = new Test();
            DateTime date = DateTime.Now;

            //Act
            t.ID = 1;
            t.Temperature = 27.001843;
            t.TimeOfDataRecording = date;
            t.RaspberryPiID = 1;
            t.HasFever = false;

            //Assert
            Assert.AreEqual(t.ID, 1);
            Assert.AreEqual(t.Temperature, 27.001843);
            Assert.AreEqual(t.TimeOfDataRecording, date);
            Assert.AreEqual(t.RaspberryPiID, 1);
            Assert.AreEqual(t.HasFever, false);
        }
    }
}
