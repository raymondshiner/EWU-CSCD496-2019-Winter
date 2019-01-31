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

        private bool GroupObjectsAreEqual(Group group1, Group group2)
        {
            return group1.Id == group2.Id && group1.Name == group2.Name;
        }
    }
}
