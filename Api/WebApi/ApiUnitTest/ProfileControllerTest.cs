using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Models;

namespace ApiUnitTest
{
    [TestClass]
    public class ProfileControllerTest
    {
        [TestMethod]
        public void TestGetAll()
        {
            //Arrange
            List<Profile> profiles = new List<Profile>();
            ProfilesController controller = new ProfilesController();
            bool check;

            //Act
            profiles = controller.Get();
            check = profiles.Count > 0;

            //Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void TestGetOne()
        {
            //Arrange
            ProfilesController controller = new ProfilesController();

            //Act
            Profile p = controller.Get(2);

            //Assert
            Assert.IsNotNull(p);
        }

        //Passed
        //[TestMethod]
        //public void TestPostOne()
        //{
        //    //Arrange
        //    Profile p = new Profile()
        //    {
        //        address = "testAd",
        //        city = "testCi",
        //        companyName = "testCN",
        //        country = "testCo",
        //        ID = 0,
        //        joinDate = DateTime.Now,
        //        phone = "1234"
        //    };
        //    ProfilesController controller = new ProfilesController();

        //    //Act
        //     CreatedAtActionResult response = (CreatedAtActionResult) controller.Post(p);
            
        //     //Assert
        //     Assert.AreEqual(response.StatusCode, 201);
        //}

        //Passed
        [TestMethod]
        public void TestUpdateOne()
        {
            //Arrange
            DateTime date = DateTime.Now;
            Profile p = new Profile()
                {
                    address = "Ad", city = "Ci", companyName = "CN", country = "Co", ID = 2,
                    joinDate = date, phone = "14"
                };
            ProfilesController controller = new ProfilesController();

            //Act
            StatusCodeResult response = (StatusCodeResult) controller.Put(2, p);
            
            //Assert
            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public void TestUpdateOneIDMismatch()
        {
            //Arrange
            DateTime date = DateTime.Now;
            Profile p = new Profile()
            {
                address = "Ad",
                city = "Ci",
                companyName = "CN",
                country = "Co",
                ID = 2,
                joinDate = date,
                phone = "14"
            };
            ProfilesController controller = new ProfilesController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(8, p);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void TestUpdateOneIDOutOfRange()
        {
            //Arrange
            DateTime date = DateTime.Now;
            Profile p = new Profile()
            {
                address = "Ad",
                city = "Ci",
                companyName = "CN",
                country = "Co",
                ID = 8,
                joinDate = date,
                phone = "14"
            };
            ProfilesController controller = new ProfilesController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(8, p);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status404NotFound);
        }

        //Passed
        //[TestMethod]
        //public void TestDeleteOne()
        //{
        //    //Arrange
        //    ProfilesController controller = new ProfilesController();

        //    //Act
        //    StatusCodeResult response = (StatusCodeResult) controller.Delete(7);

        //    //Assert
        //    Assert.AreEqual(response.StatusCode, 200);
        //}

        [TestMethod]
        public void TestDeleteOneIDOutOfRange()
        {
            //Arrange
            ProfilesController controller = new ProfilesController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Delete(8);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status404NotFound);
        }
    }
}
