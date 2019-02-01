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
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresUserService()
        {
            new GroupController(null);
        }

        [TestMethod]
        public void GetGroup_ReturnsAGroup()
        {
            var group = new Group
            {
                Name = "Fun Group",
                Id = 4
            };

            var testService = new TestableGroupService
            {
                GetGroup_Return = group
            };

            var controller = new GroupController(testService);
            var result = controller.GetGroup(4);
            bool res = GroupObjectsAreEqual(DTO.Group.ToEntity(result.Value), group);

            Assert.IsTrue(res);
            Assert.AreEqual(4, testService.GetGroup_GroupId);
        }

        [TestMethod]
        public void GetGroup_NegativeGroupIdReturnsNotFound()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.GetGroup(-1);

            Assert.IsTrue(result.Result is NotFoundResult);
            Assert.AreEqual(0, testService.GetGroup_GroupId);
        }

        [TestMethod]
        public void AddGroup_AddsAGroup()
        {
            var group = new Group
            {
                Name = "Fun Group",
                Id = 4
            };

            var testService = new TestableGroupService
            {
                AddGroup_Return =  group
            };

            var controller = new GroupController(testService);
            var result = controller.AddGroup(new DTO.Group(group));
            bool res = GroupObjectsAreEqual(DTO.Group.ToEntity(result.Value), group);
            bool serviceInvoked = GroupObjectsAreEqual(testService.AddGroup_Group, group);

            Assert.IsTrue(res);
            Assert.IsTrue(serviceInvoked);
        }

        [TestMethod]
        public void AddGroup_NullGroupReturnsBadRequest()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.AddGroup(null);

            Assert.IsTrue(result.Result is BadRequestResult);
            Assert.IsNull(testService.AddGroup_Group);
        }

        [TestMethod]
        public void UpdateGroup_UpdatesAGroup()
        {
            var group = new Group
            {
                Name = "Fun Group",
                Id = 4
            };

            var testService = new TestableGroupService
            {
                UpdateGroup_Return = group
            };

            var controller = new GroupController(testService);
            var result = controller.UpdateGroup(new DTO.Group(group));
            bool res = GroupObjectsAreEqual(DTO.Group.ToEntity(result.Value), group);
            bool serviceInvoked = GroupObjectsAreEqual(testService.UpdateGroup_Group, group);

            Assert.IsTrue(res);
            Assert.IsTrue(serviceInvoked);
        }

        [TestMethod]
        public void UpdateGroup_NullGroupReturnsBadRequest()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.UpdateGroup(null);

            Assert.IsTrue(result.Result is BadRequestResult);
            Assert.IsNull(testService.UpdateGroup_Group);
        }

        [TestMethod]
        public void DeleteGroup_DeletesAGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.DeleteGroup(4);

            Assert.IsTrue(result is OkResult);
            Assert.AreEqual(4, testService.DeleteGroup_GroupId);
        }

        [TestMethod]
        public void DeleteGroup_NegativeGroupIdReturnsNotFound()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.DeleteGroup(-1);

            Assert.IsTrue(result is NotFoundResult);
            Assert.AreEqual(0, testService.DeleteGroup_GroupId);
        }

        [TestMethod]
        public void AddUserToGroup_UserIsAddedToGroup()
        {
            var user = new User
            {
                Id = 4,
                FirstName = "Ray",
                LastName = "Pizza"
            };

            var testService = new TestableGroupService
            {
                AddUserToGroup_User = user
            };
            var controller = new GroupController(testService);
            var result = controller.AddUserToGroup(4, new DTO.User(user));

            Assert.IsTrue(result is OkResult);
            Assert.AreEqual(4, testService.AddUserToGroup_GroupId);
            Assert.AreEqual(user.FirstName, testService.AddUserToGroup_User.FirstName);
        }

        [TestMethod]
        public void AddUserToGroup_NegativeGroupIdReturnsNotFoundResult()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.AddUserToGroup(-1, new DTO.User());

            Assert.IsTrue(result is NotFoundResult);
            Assert.AreEqual(0, testService.AddUserToGroup_GroupId);
        }

        [TestMethod]
        public void AddUserToGroup_NullUserReturnsBadRequestResult()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);
            var result = controller.AddUserToGroup(4, null);

            Assert.IsTrue(result is BadRequestResult);
            Assert.AreEqual(0, testService.AddUserToGroup_GroupId);
        }

        [TestMethod]
        public void RemoveUserFromGroup_UserIsRemoved()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            int groupId = 2;
            int userId = 5;
            var result = controller.RemoveUserFromGroup(userId, groupId);

            Assert.IsTrue(result is OkResult);
            Assert.AreEqual(groupId, testService.RemoveUserFromGroup_GroupId);
            Assert.AreEqual(userId, testService.RemoveUserFromGroup_UserId);
        }

        [TestMethod]
        public void RemoveUserFromGroup_NegativeUserIdReturnsNotFoundResult()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            int groupId = 2;
            int userId = -5;
            var result = controller.RemoveUserFromGroup(userId, groupId);

            Assert.IsTrue(result is NotFoundResult);
            Assert.AreEqual(0, testService.RemoveUserFromGroup_GroupId);
            Assert.AreEqual(0, testService.RemoveUserFromGroup_UserId);
        }

        [TestMethod]
        public void RemoveUserFromGroup_NegativeGroupIdReturnsNotFoundResult()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            int groupId = -2;
            int userId = 5;
            var result = controller.RemoveUserFromGroup(userId, groupId);

            Assert.IsTrue(result is NotFoundResult);
            Assert.AreEqual(0, testService.RemoveUserFromGroup_GroupId);
            Assert.AreEqual(0, testService.RemoveUserFromGroup_UserId);
        }

        [TestMethod]
        public void GetAllGroups_ReturnsAllGroups()
        {
            var testService = new TestableGroupService
            {
                GetAllGroups_Return = new List<Group>()
            };

            var group = new Group
            {
                Id = 4,
                Name = "Levels"
            };

            testService.GetAllGroups_Return.Add(group);

            var controller = new GroupController(testService);

            var result = controller.GetAllGroups();

            Assert.AreEqual(group.Id, result.Value[0].Id);
            Assert.AreEqual(group.Name, result.Value[0].Name);
            Assert.IsTrue(testService.GetAllGroups_ServiceInvoked);
        }

        [TestMethod]
        public void GetAllUsersInGroup_ReturnsAllUsers()
        {
            var user = new User
            {
                Id = 4,
                FirstName = "Ray"
            };

            var testService = new TestableGroupService
            {
                GetAllUsersInGroup_Return = new List<User> {user}
            };;

            var controller = new GroupController(testService);

            var result = controller.GetAllUsersInGroup(1);

            Assert.AreEqual(user.Id, result.Value[0].Id);
            Assert.AreEqual(user.FirstName, result.Value[0].FirstName);
            Assert.AreEqual(1, testService.GetAllUsersInGroup_GroupId);
        }

        [TestMethod]
        public void GetAllUsersInGroup_NegativeGroupIdReturnsNotFoundResult()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.GetAllUsersInGroup(-4);

            Assert.IsTrue(result.Result is NotFoundResult);
            Assert.AreEqual(0, testService.GetAllUsersInGroup_GroupId);
        }

        private bool GroupObjectsAreEqual(Group group1, Group group2)
        {
            return group1.Id == group2.Id && group1.Name == group2.Name;
        }
    }
}
