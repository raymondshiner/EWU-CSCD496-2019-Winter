using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresUserService()
        {
            new UserController(null);
        }

        [TestMethod]
        public void GetUser_ReturnsAUser()
        {
            var user = new User
            {
                Id = 4,
                FirstName = "Raymond",
                LastName = "Shiner"
            };

            var testService = new TestableUserService
            {
                GetUser_Return = user
            };

            var controller = new UserController(testService);
            var result = controller.GetUser(4);
            bool res = UserObjectsAreEqual(DTO.User.ToEntity(result.Value), user);

            Assert.IsTrue(res);
            Assert.AreEqual(4, testService.GetUser_UserId);
        }

        [TestMethod]
        public void GetUser_NegativeUserIdReturnsNotFound()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);
            var result = controller.GetUser(-1);

            Assert.IsTrue(result.Result is NotFoundResult);
            Assert.AreEqual(0, testService.GetUser_UserId);
        }

        [TestMethod]
        public void CreateUser_AddsAUser()
        {
            var user = new User
            {
                Id = 4,
                FirstName = "Raymond",
                LastName = "Shiner"
            };

            var testService = new TestableUserService
            {
                AddUser_Return = user
            };

            var controller = new UserController(testService);
            
            var result = controller.CreateUser(new DTO.User(user));
            bool res = UserObjectsAreEqual(DTO.User.ToEntity(result.Value), user);
            bool serviceInvoked = UserObjectsAreEqual(user, testService.AddUser_User);

            Assert.IsTrue(res);
            Assert.IsTrue(serviceInvoked);
        }

        [TestMethod]
        public void CreateUser_NullUserReturnsBadRequest()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            var result = controller.CreateUser(null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateUser_UserIsUpdated()
        {
            var user = new User
            {
                Id = 4,
                FirstName = "Raymond",
                LastName = "Shiner"
            };

            var testService = new TestableUserService
            {
                UpdateUser_Return = user
            };

            var controller = new UserController(testService);

            var result = controller.UpdateUser(4, new DTO.User(user));
            bool res = UserObjectsAreEqual(DTO.User.ToEntity(result.Value), user);
            bool serviceInvoked = UserObjectsAreEqual(user, testService.UpdateUser_User);

            Assert.IsTrue(res);
            Assert.IsTrue(serviceInvoked);
        }

        [TestMethod]
        public void UpdateUser_NullUserReturnsBadRequest()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            var result = controller.UpdateUser(1, null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateUser_NegativeUserIdReturnsNotFound()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);
            var result = controller.UpdateUser(-1, new DTO.User());

            Assert.IsTrue(result.Result is NotFoundResult);
            Assert.IsNull(testService.UpdateUser_User);
        }

        [TestMethod]
        public void DeleteUser_UserIsDeleted()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            controller.DeleteUser(4);

            Assert.AreEqual(4, testService.DeleteUser_UserId);
        }

        [TestMethod]
        public void DeleteUser_NegativeUserIdReturnsNotFound()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);
            var result = controller.DeleteUser(-1);

            Assert.IsTrue(result is NotFoundResult);
            Assert.AreEqual(0, testService.DeleteUser_UserId);
        }

        private bool UserObjectsAreEqual(User user1, User user2)
        {
            return user1.Id == user2.Id
                   && user1.FirstName == user2.FirstName
                   && user1.LastName == user2.LastName;
        }
    }
}
