using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.DriverPages;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class GroupPageTests
    {
        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void Init()
        {
            Driver = new ChromeDriver(Path.GetFullPath("."));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        public void CanGetToGroupsPage()
        {
            //Arrange
            Driver.Navigate().GoToUrl(HomePage.Path);

            //Act
            var homePage = new HomePage(Driver);
            homePage.GroupsLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GroupsPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddGroupsPage()
        {
            //Arrange
            var rootUri = new Uri(HomePage.Path);
            Driver.Navigate().GoToUrl(GroupsPage.Path);
            var page = new GroupsPage(Driver);

            //Act
            page.AddGroup.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(AddGroupsPage.Slug));
        }

        [TestMethod]
        public void CanAddGroups()
        {
            //Arrange /Act
            string groupName = "Group Name" + Guid.NewGuid().ToString("N");
            GroupsPage page = CreateGroup(groupName);
            
            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GroupsPage.Slug));
            List<string> groupNames = page.GroupNames;
            Assert.IsTrue(groupNames.Contains(groupName));
        }

        [TestMethod]
        public void CanDeleteGroup()
        {
            //Arrange
            string groupName = "Group Name" + Guid.NewGuid().ToString("N");
            GroupsPage page = CreateGroup(groupName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(groupName);
            deleteLink.Click();
            Driver.SwitchTo().Alert().Accept();

            //Assert
            List<string> groupNames = page.GroupNames;
            Assert.IsFalse(groupNames.Contains(groupName));
        }

        private GroupsPage CreateGroup(string groupName)
        {
            Driver.Navigate().GoToUrl(AddGroupsPage.Path);
            var groupsPage = new GroupsPage(Driver);
            var addGroupPage = new AddGroupsPage(Driver);
            
            addGroupPage.GroupNameTextBox.SendKeys(groupName);
            addGroupPage.SubmitButton.Click();

            return groupsPage;
        }
    }
}
