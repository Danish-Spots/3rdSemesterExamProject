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
            Session p = controller.Get(1);

            //Assert
            Assert.IsNotNull(p);
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
        //        UserID = 11
        //    };
        //    SessionsController controller = new SessionsController();

        //    //Act
        //    StatusCodeResult response = (StatusCodeResult)controller.Post(s);

        //    //Assert
        //    Assert.AreEqual(response.StatusCode, 201);
        //}

        [TestMethod]
        public void TestPostOneConflict()
        {
            //Arrange
            Session s = new Session()
            {
                ID = 1,
                Key = "asljdhliajhbdkjeblifauh209837",
                UserID = 9
            };
            SessionsController controller = new SessionsController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Post(s);

            //Assert
            Assert.AreEqual(response.StatusCode, 409);
        }


        //Passed
        //[TestMethod]
        //public void TestUpdateOne()
        //{
        //    //Arrange
            
        //    Session s = new Session()
        //    {
        //        Key = "changedtext",
        //        ID = 2,
        //        UserID = 11
        //    };
        //    SessionsController controller = new SessionsController();

        //    //Act
        //    StatusCodeResult response = (StatusCodeResult)controller.Put(2, s);

        //    //Assert
        //    Assert.AreEqual(response.StatusCode, 200);
        //}

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
        //    StatusCodeResult response = (StatusCodeResult)controller.Delete(2);

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
    }
}
