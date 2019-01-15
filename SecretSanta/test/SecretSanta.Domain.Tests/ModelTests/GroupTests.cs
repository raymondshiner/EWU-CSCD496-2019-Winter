using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.ModelTests
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public void DefaultConstructor_ReturnsGroupObject()
        {
            Group group = new Group();
            Assert.IsTrue(group is Group);
        }

        [TestMethod]
        public void DefaultConstructor_InheritsFromEntity()
        {
            Group group = new Group();
            Assert.IsTrue(group is Entity);
        }

        [TestMethod]
        public void SetAndGetTitle_ReturnsSetTitle()
        {
            Group group = new Group();
            string expectedTitle = "No one expects the spanish inquisition!";
            group.Title = expectedTitle;
            Assert.AreEqual(expectedTitle, group.Title);
        }

        [TestMethod]
        public void SetAndGetID_ReturnsSetID()
        {
            Group group = new Group();
            int expectedID = 42;
            group.Id = expectedID;
            Assert.AreEqual(expectedID, group.Id);
        }

        [TestMethod]
        public void SetUsers_SetsSuccessfully()
        {
            Group group = new Group();
            List<User> userList = new List<User>();
            group.Users = userList;
            Assert.AreEqual(userList, group.Users);
        }
    }
}
