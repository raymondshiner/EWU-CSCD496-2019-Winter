using System;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.DriverPages
{
    public class EditUsersPage
    {
        public const string Path = UsersPage.Path + "Edit/";
        public const string Slug = UsersPage.Slug + "/Edit";
        public IWebDriver Driver { get; }
        public EditUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public IWebElement FirstNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement LastNameTextBox => Driver.FindElement(By.Id("LastName"));
        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public string GetUserId => Driver.Url.Substring(Driver.Url.LastIndexOf("/") + 1);
    }
}