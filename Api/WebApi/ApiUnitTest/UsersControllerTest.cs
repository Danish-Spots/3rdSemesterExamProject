﻿using System;
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
    public class UsersControllerTest
    {
        [TestMethod]
        public void TestGetAll()
        {
            //Arrange
            List<User> users = new List<User>();
            UsersController controller = new UsersController();
            bool check;

            //Act
            users = controller.Get();
            check = users.Count > 0;

            //Assert
            Assert.AreEqual(check, true);
        }



        [TestMethod]
        public void TestGetOne()
        {
            //Arrange
            UsersController controller = new UsersController();

            //Act
            IActionResult p = controller.Get(9);
            OkObjectResult result = p as OkObjectResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [TestMethod]
        public void TestGetOneUserName()
        {
            //Arrange
            UsersController controller = new UsersController();

            //Act
            IActionResult p = controller.Get("testUser5");
            OkObjectResult result = p as OkObjectResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [TestMethod]
        public void TestGetUserNameNotFound()
        {
            //Arrange
            UsersController controller = new UsersController();

            //Act
            IActionResult p = controller.Get("testUser10");
            StatusCodeResult result = p as StatusCodeResult;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void TestGetOneIDOutOfRange()
        {
            //Arrange
            UsersController controller = new UsersController();

            //Act
            IActionResult p = controller.Get(93);
            StatusCodeResult result = p as StatusCodeResult;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 404);
        }

        //passed
        //[TestMethod]
        //public void TestPostOne()
        //{
        //    //Arrange
        //    User p = new User()
        //    {
        //        UserName = "testUser5",
        //        Password = "testPassword",
        //        Email = "test@test.com",
        //        ID = 1,
        //        ProfileID = 2
        //    };
        //    UsersController controller = new UsersController();

        //    //Act
        //    IActionResult response = controller.Post(p);
        //    CreatedAtActionResult result = response as CreatedAtActionResult;
        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 201);
        //}

        [TestMethod]
        public void TestPostOneUserNameConflict()
        {
            //Arrange
            User p = new User()
            {
                UserName = "testUser",
                Password = "testPassword",
                Email = "test@test.com",
                ID = 1,
                ProfileID = 2
            };
            UsersController controller = new UsersController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Post(p);

            //Assert
            Assert.AreEqual(response.StatusCode, 409);
        }

        [TestMethod]
        public void TestPostOneProfileIDOutOfRange()
        {
            //Arrange
            User p = new User()
            {
                UserName = "testUserdfw",
                Password = "testPassword",
                Email = "test@test.com",
                ID = 1,
                ProfileID = 2123
            };
            UsersController controller = new UsersController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Post(p);

            //Assert
            Assert.AreEqual(response.StatusCode, 400);
        }

        //Passed
        [TestMethod]
        public void TestUpdateOne()
        {
            //Arrange
            DateTime date = DateTime.Now;
            User u = new User()
            {
                UserName = "testUse1r",
                Password = "testPasswor1d",
                Email = "test@test.com1",
                ID = 11,
                ProfileID = 2
            };
            UsersController controller = new UsersController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Put(11, u);

            //Assert
            Assert.AreEqual(response.StatusCode, 200);
        }

        [TestMethod]
        public void TestUpdateOneIDMismatch()
        {
            //Arrange
            DateTime date = DateTime.Now;
            User u = new User()
            {
                UserName = "testUse1r",
                Password = "testPasswor1d",
                Email = "test@test.com1",
                ID = 1,
                ProfileID = 2
            };
            UsersController controller = new UsersController();

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
            User u = new User()
            {
                UserName = "testUse1r",
                Password = "testPasswor1d",
                Email = "test@test.com1",
                ID = 8,
                ProfileID = 2
            };
            UsersController controller = new UsersController();

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
        //    UsersController controller = new UsersController();

        //    //Act
        //    StatusCodeResult response = (StatusCodeResult)controller.Delete(10);

        //    //Assert
        //    Assert.AreEqual(response.StatusCode, 200);
        //}

        [TestMethod]
        public void TestDeleteOneIDOutOfRange()
        {
            //Arrange
            UsersController controller = new UsersController();

            //Act
            StatusCodeResult response = (StatusCodeResult)controller.Delete(8);

            //Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status404NotFound);
        }
    }
}
