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
            IActionResult p = controller.Get(1);
            OkObjectResult result = p as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [TestMethod]
        public void TestGetOneIDNotFound()
        {
            //Arrange
            RaspberryPisController controller = new RaspberryPisController();

            //Act
            IActionResult p = controller.Get(50);
            NotFoundResult result = p as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 404);
        }

        //Passed
        //[TestMethod]
        //public void TestPostOne()
        //{
        //    //Arrange
        //    RaspberryPi p = new RaspberryPi()
        //    {
        //        Location = "Test Location 3",
        //        IsActive = false,
        //        ID = 0,
        //        ProfileID = 2
        //    };
        //    RaspberryPisController controller = new RaspberryPisController();

        //    //Act
        //    CreatedAtActionResult response = (CreatedAtActionResult)controller.Post(p);

        //    //Assert
        //    Assert.IsNotNull(response);
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
                ID = 3,
                ProfileID = 2
            };
            RaspberryPisController controller = new RaspberryPisController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(3, p);

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
        //    StatusCodeResult response = (StatusCodeResult)controller.Delete(3);

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
