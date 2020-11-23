using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;

namespace ApiUnitTest
{
    [TestClass]
    public class ProfileTest
    {
        [TestMethod]
        public void TestID()
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
    }
}
