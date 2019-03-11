using System;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.DriverPages
{
    public class HomePage
    {
        public const string Path = "https://localhost:44349/";
        public const string Slug = "";
        public IWebDriver Driver { get; }
        public HomePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public GroupsPage GroupPage => new GroupsPage(Driver);

        public IWebElement GroupsLink => Driver.FindElement(By.LinkText("Groups"));

        public IWebElement UsersLink => Driver.FindElement(By.LinkText("Users"));
    }
}