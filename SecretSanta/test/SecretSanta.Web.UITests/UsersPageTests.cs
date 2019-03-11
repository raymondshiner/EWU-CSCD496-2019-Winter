using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.DriverPages;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UsersPageTests
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
        public void CanNavigateToUsersPage()
        {
            Driver.Navigate().GoToUrl(HomePage.Path);

            var homePage = new HomePage(Driver);
            homePage.UsersLink.Click();

            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddUsersPage()
        {
            Driver.Navigate().GoToUrl(UsersPage.Path);

            var usersPage = new UsersPage(Driver);
            usersPage.AddUserLink.Click();

            Assert.IsTrue(Driver.Url.EndsWith(AddUsersPage.Slug));
        }

        [TestMethod]
        public void CanAddUser()
        {
            var firstName = "First Name";
            var lastName = "Last Name" + Guid.NewGuid().ToString("N");
            var fullName = firstName + " " + lastName;

            Driver.Navigate().GoToUrl(AddUsersPage.Path);
            var usersPage = CreateUser(firstName, lastName);

            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            var userNameList = usersPage.UserNames;
            Assert.IsTrue(userNameList.Contains(fullName));
        }

        private UsersPage CreateUser(string firstName, string lastName)
        {
            Driver.Navigate().GoToUrl(AddUsersPage.Path);
            var usersPage = new UsersPage(Driver);
            var addUsersPage = new AddUsersPage(Driver);

            addUsersPage.FirstNameTextBox.SendKeys(firstName);
            addUsersPage.LastNameTextBox.SendKeys(lastName);
            addUsersPage.SubmitButton.Click();

            return usersPage;
        }
    }
}