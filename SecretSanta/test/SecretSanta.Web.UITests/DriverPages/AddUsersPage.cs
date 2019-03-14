using System;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.DriverPages
{
    public class AddUsersPage
    {
        public const string Path = UsersPage.Path + "Add/";
        public const string Slug = UsersPage.Slug + "/Add";
        public IWebDriver Driver { get; }
        public AddUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public IWebElement FirstNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement LastNameTextBox => Driver.FindElement(By.Id("LastName"));


        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public IWebElement GetNavbarLink(string linkName) => Driver.FindElement(By.LinkText(linkName));

        public IWebElement GetFirstNameRequiredNotification => Driver.FindElement(By.CssSelector("ul>li"));
    }
}