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
        public void CanNavigateToEditUsersPage()
        {
            var firstName = "First Name";
            var lastName = "Last Name" + Guid.NewGuid().ToString("N");
            var fullName = firstName + " " + lastName;

            var usersPage = CreateUser(firstName, lastName);
            var editLink = usersPage.GetEditLink(fullName);
            
            editLink.Click();

            var editPage = new EditUsersPage(Driver);

            Assert.IsTrue(Driver.Url.EndsWith(EditUsersPage.Slug + "/" + editPage.GetUserId));
        }

        [DataRow("Secret Santa")]
        [DataRow("Groups")]
        [TestMethod]
        public void CanNavigateFromUsersPageToNavBarPages(string linkName)
        {
            Driver.Navigate().GoToUrl(UsersPage.Path);
            var page = new UsersPage(Driver);
            var navbarLink = page.GetNavbarLink(linkName);
            navbarLink.Click();

            if (linkName == "Secret Santa")
            {
                Assert.IsTrue(Driver.Url == HomePage.Path);
            }

            else
            {
                Assert.IsTrue(Driver.Url.EndsWith(linkName));
            }
        }


        [DataRow("Secret Santa")]
        [DataRow("Groups")]
        [DataRow("Users")]
        [TestMethod]
        public void CanNavigateFromAddUsersPageToNavBarPages(string linkName)
        {
            Driver.Navigate().GoToUrl(AddUsersPage.Path);
            var page = new AddUsersPage(Driver);
            var navbarLink = page.GetNavbarLink(linkName);
            navbarLink.Click();

            if (linkName == "Secret Santa")
            {
                Assert.IsTrue(Driver.Url == HomePage.Path);
            }

            else
            {
                Assert.IsTrue(Driver.Url.EndsWith(linkName));
            }
        }



        [TestMethod]
        public void CanAddUser()
        {
            var firstName = "First Name";
            var lastName = "Last Name" + Guid.NewGuid().ToString("N");
            var fullName = firstName + " " + lastName;

            var usersPage = CreateUser(firstName, lastName);

            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            var userNameList = usersPage.UserNames;
            Assert.IsTrue(userNameList.Contains(fullName));
        }

        [TestMethod]
        public void FirstNameValidationRequired()
        {
            Driver.Navigate().GoToUrl(AddUsersPage.Path);
            var page = new AddUsersPage(Driver);

            page.LastNameTextBox.SendKeys("lastName");
            page.SubmitButton.Click();

            var notification = page.GetFirstNameRequiredNotification;

            Assert.IsTrue(notification.Text == "The FirstName field is required.");
        }


        [TestMethod]
        public void CanEditUser()
        {
            var firstName = "First Name";
            var lastName = "Last Name" + Guid.NewGuid().ToString("N");
            var fullName = firstName + " " + lastName;

            var usersPage = CreateUser(firstName, lastName);
            var editUsersPage = new EditUsersPage(Driver);

            var editLink = usersPage.GetEditLink(fullName);
            editLink.Click();
            
            var newFirstName = "New Name";
            var newFullName = newFirstName + " " + lastName;

            editUsersPage.FirstNameTextBox.Clear();
            editUsersPage.FirstNameTextBox.SendKeys(newFirstName);
            editUsersPage.LastNameTextBox.Clear();
            editUsersPage.LastNameTextBox.SendKeys(lastName);

            editUsersPage.SubmitButton.Click();
            
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            Assert.IsTrue(usersPage.UserNames.Contains(newFullName));
            Assert.IsFalse(usersPage.UserNames.Contains(fullName));
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            var firstName = "First Name";
            var lastName = "Last Name" + Guid.NewGuid().ToString("N");
            var fullName = firstName + " " + lastName;

            var usersPage = CreateUser(firstName, lastName);

            Driver.Navigate().GoToUrl(UsersPage.Path);
            usersPage.GetDeleteLink(fullName).Click();
            Driver.SwitchTo().Alert().Accept();

            var userNames = usersPage.UserNames;

            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            Assert.IsFalse(userNames.Contains(fullName));

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