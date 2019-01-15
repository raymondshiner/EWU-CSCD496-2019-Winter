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
        public void Constructor_ReturnsGroupObject()
        {
            Group group = new Group("Humans");
            Assert.IsTrue(group is Group);
        }

        [TestMethod]
        public void Constructor_InheritsFromEntity()
        {
            Group group = new Group("Humans");
            Assert.IsTrue(group is Entity);
        }

        [TestMethod]
        public void Constructor_TitleSetCorrectly()
        {
            Group group = new Group("Humans");
            Assert.AreEqual("Humans", group.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullTitle_ThrowException()
        {
            Group group = new Group(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyTitle_ThrowException()
        {
            Group group = new Group("");
        }

        [TestMethod]
        public void SetAndGetTitle_ReturnsSetTitle()
        {
            Group group = new Group("Humans");
            string expectedTitle = "No one expects the spanish inquisition!";
            group.Title = expectedTitle;
            Assert.AreEqual(expectedTitle, group.Title);
        }

        [TestMethod]
        public void SetAndGetID_ReturnsSetID()
        {
            Group group = new Group("Humans");
            int expectedID = 42;
            group.Id = expectedID;
            Assert.AreEqual(expectedID, group.Id);
        }

        [TestMethod]
        public void SetUsers_SetsSuccessfully()
        {
            Group group = new Group("Humans");
            group.Users?.Add(new User());
            List<User> userList = new List<User>();
            group.Users = userList;
            Assert.AreEqual(userList, group.Users);
            Assert.AreEqual(0, group.Users.Count);
        }
    }
}
