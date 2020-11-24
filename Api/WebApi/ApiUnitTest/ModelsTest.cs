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
            profile.companyName = "testCN";
            profile.city = "testCi";
            profile.joinDate = date;
            profile.phone = "2139";
            profile.address = "testAd";
            profile.country = "testCo";
            //Assert
            Assert.AreEqual(profile.ID, 0);
            Assert.AreEqual(profile.companyName, "testCN");
            Assert.AreEqual(profile.city, "testCi");
            Assert.AreEqual(profile.joinDate, date);
            Assert.AreEqual(profile.phone, "2139");
            Assert.AreEqual(profile.address, "testAd");
            Assert.AreEqual(profile.country, "testCo");
        }

        [TestMethod]
        public void TestUser()
        {
            //Arrange
            User u = new User();

            //Act
            u.ID = 1;
            u.email = "testemail";
            u.userName = "testusername";
            u.password = "testpassword";
            u.profileID = 1;

            //Assert
            Assert.AreEqual(u.ID, 1);
            Assert.AreEqual(u.email, "testemail");
            Assert.AreEqual(u.userName, "testusername");
            Assert.AreEqual(u.password, "testpassword");
            Assert.AreEqual(u.profileID, 1);
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
    }
}
