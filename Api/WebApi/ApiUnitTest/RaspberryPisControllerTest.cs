using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Models;

namespace ApiUnitTest
{
    [TestClass]
    public class RaspberryPisControllerTest
    {
        [TestMethod]
        public void TestGetAll()
        {
            //Arrange
            List<RaspberryPi> pis = new List<RaspberryPi>();
            RaspberryPisController controller = new RaspberryPisController();
            bool check;

            //Act
            pis = controller.Get();
            check = pis.Count > 0;

            //Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void TestGetOne()
        {
            //Arrange
            RaspberryPisController controller = new RaspberryPisController();

            //Act
            RaspberryPi p = controller.Get(1);

            //Assert
            Assert.IsNotNull(p);
        }

        //Passed
        //[TestMethod]
        //public void TestPostOne()
        //{
        //    //Arrange
        //    RaspberryPi p = new RaspberryPi()
        //    {
        //        Location = "Test Location 2",
        //        IsActive = false,
        //        ID = 1,
        //        ProfileID = 2
        //    };
        //    RaspberyPisController controller = new RaspberyPisController();

        //    //Act
        //    CreatedAtActionResult response = (CreatedAtActionResult)controller.Post(p);

        //    //Assert
        //    Assert.AreEqual(response.StatusCode, 201);
        //}


        [TestMethod]
        public void TestUpdateOne()
        {
            //Arrange
            RaspberryPi p = new RaspberryPi()
            {
                Location = "Test Location Updated",
                IsActive = false,
                ID = 1,
                ProfileID = 2
            };
            RaspberryPisController controller = new RaspberryPisController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(1, p);

            //Assert
            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public void TestUpdateOneIDMismatch()
        {
            //Arrange
            RaspberryPi u = new RaspberryPi()
            {
                Location = "Test Location Updated",
                IsActive = false,
                ID = 1,
                ProfileID = 2
            };
            RaspberryPisController controller = new RaspberryPisController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(9, u);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void TestUpdateOneIDOutOfRange()
        {
            //Arrange
            RaspberryPi u = new RaspberryPi()
            {
                Location = "Test Location Updated",
                IsActive = false,
                ID = 8,
                ProfileID = 2
            };
            RaspberryPisController controller = new RaspberryPisController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(8, u);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status404NotFound);
        }

        //Passed
        //[TestMethod]
        //public void TestDeleteOne()
        //{
        //    //Arrange
        //    RaspberryPisController controller = new RaspberryPisController();

        //    //Act
        //    StatusCodeResult response = (StatusCodeResult)controller.Delete(2);

        //    //Assert
        //    Assert.AreEqual(response.StatusCode, 200);
        //}

        [TestMethod]
        public void TestDeleteOneIDOutOfRange()
        {
            //Arrange
            RaspberryPisController controller = new RaspberryPisController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Delete(8);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status404NotFound);
        }
    }
}
