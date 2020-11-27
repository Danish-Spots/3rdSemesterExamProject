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
    public class SessionsControllerTest
    {
        [TestMethod]
        public void TestGetAll()
        {
            //Arrange
            List<Session> sessions = new List<Session>();
            SessionsController controller = new SessionsController();
            bool check;

            //Act
            sessions = controller.Get();
            check = sessions.Count > 0;

            //Assert
            Assert.AreEqual(check, true);
        }

        [TestMethod]
        public void TestGetOne()
        {
            //Arrange
            SessionsController controller = new SessionsController();

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
            SessionsController controller = new SessionsController();

            //Act
            IActionResult p = controller.Get(123);
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
        //    Session s = new Session()
        //    {
        //        ID = 1,
        //        Key = "asljdhliajhbdkjh209837",
        //        UserID = 12
        //    };
        //    SessionsController controller = new SessionsController();

        //    //Act
        //    IActionResult response = controller.Post(s);
        //    CreatedAtActionResult result = response as CreatedAtActionResult;
        //    //Assert
        //    Assert.AreEqual(result.StatusCode, 201);
        //}

        [TestMethod]
        public void TestPostOneIDInUse()
        {
            //Arrange
            Session s = new Session()
            {
                ID = 1,
                Key = "asljdhliajhbdkjh209837",
                UserID = 11
            };
            SessionsController controller = new SessionsController();

            //Act
            IActionResult response = controller.Post(s);
            StatusCodeResult result = response as StatusCodeResult;
            //Assert
            Assert.AreEqual(result.StatusCode, 409);
        }

        [TestMethod]
        public void TestPostOneUserIDOutOfRange()
        {
            //Arrange
            Session s = new Session()
            {
                ID = 1,
                Key = "asljdhliajhbdkjh209837",
                UserID = 12313
            };
            SessionsController controller = new SessionsController();

            //Act
            IActionResult response = controller.Post(s);
            StatusCodeResult result = response as StatusCodeResult;
            //Assert
            Assert.AreEqual(result.StatusCode, 400);
        }


        //Passed
        [TestMethod]
        public void TestUpdateOne()
        {
            //Arrange

            Session s = new Session()
            {
                Key = "changedtext",
                ID = 5,
                UserID = 11
            };
            SessionsController controller = new SessionsController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(5, s);

            //Assert
            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public void TestUpdateOneIDMismatch()
        {
            //Arrange
            Session s = new Session()
            {
                Key = "changedtext",
                ID = 2,
                UserID = 11
            };
            SessionsController controller = new SessionsController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(9, s);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status400BadRequest);
        }

        [TestMethod]
        public void TestUpdateOneIDOutOfRange()
        {
            //Arrange
            Session s = new Session()
            {
                Key = "changedtext",
                ID = 8,
                UserID = 11
            };
            SessionsController controller = new SessionsController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(8, s);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status404NotFound);
        }

        //Passed
        //[TestMethod]
        //public void TestDeleteOne()
        //{
        //    //Arrange
        //    SessionsController controller = new SessionsController();

        //    //Act
        //    StatusCodeResult response = (StatusCodeResult)controller.Delete(3);

        //    //Assert
        //    Assert.AreEqual(response.StatusCode, 200);
        //}

        [TestMethod]
        public void TestDeleteOneIDOutOfRange()
        {
            //Arrange
            SessionsController controller = new SessionsController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Delete(8);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status404NotFound);
        }

        [TestMethod]
        public void TestGetSessionKey()
        {
            //Arrange
            SessionsController s = new SessionsController();

            //Act
            OkObjectResult response = (OkObjectResult) s.GetSK("Mesdad");


            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, 200);
        }
    }
}
