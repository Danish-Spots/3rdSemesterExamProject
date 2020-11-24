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
    public class TestsControllerTest
    {
        [TestMethod]
        public void TestGetAll()
        {
            //Arrange
            List<Test> tests = new List<Test>();
            TestsController controller = new TestsController();
            bool check;

            //Act
            tests = controller.Get();
            check = tests.Count > 0;

            //Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void TestGetOne()
        {
            //Arrange
            TestsController controller = new TestsController();

            //Act
            Test p = controller.Get(3);

            //Assert
            Assert.IsNotNull(p);
        }


        [TestMethod]
        public void TestPostOne()
        {
            //Arrange
            DateTime date = DateTime.Now;
            Test t = new Test()
            {
                Temperature = 35.029838,
                TimeOfDataRecording = date,
                ID = 1,
                HasFever = false,
                RaspberryPiID = 1,
            };
            TestsController controller = new TestsController();

            //Act
            CreatedAtActionResult response = (CreatedAtActionResult)controller.Post(t);

            //Assert
            Assert.AreEqual(response.StatusCode, 201);
        }


        [TestMethod]
        public void TestUpdateOne()
        {
            //Arrange
            DateTime date = DateTime.Now;
            Test t = new Test()
            {
                Temperature = 37.029838,
                TimeOfDataRecording = date,
                ID = 3,
                HasFever = true,
                RaspberryPiID = 1,
            };
            TestsController controller = new TestsController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(3, t);

            //Assert
            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public void TestUpdateOneIDMismatch()
        {
            //Arrange
            DateTime date = DateTime.Now;
            Test u = new Test()
            {
                Temperature = 37.029838,
                TimeOfDataRecording = date,
                ID = 3,
                HasFever = true,
                RaspberryPiID = 1,
            };
            TestsController controller = new TestsController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(9, u);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void TestUpdateOneIDOutOfRange()
        {
            //Arrange
            DateTime date = DateTime.Now;
            Test u = new Test()
            {
                Temperature = 37.029838,
                TimeOfDataRecording = date,
                ID = 8,
                HasFever = true,
                RaspberryPiID = 1,
            };
            TestsController controller = new TestsController();

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
        //    TestsController controller = new TestsController();

        //    //Act
        //    StatusCodeResult response = (StatusCodeResult)controller.Delete(4);

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
