using System;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.DriverPages
{
    public class AddGroupsPage
    {
        public const string Path = GroupsPage.Path + "Add/";
        public const string Slug = GroupsPage.Slug + "/Add";
        public IWebDriver Driver { get; }
        public AddGroupsPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public IWebElement GroupNameTextBox => Driver.FindElement(By.Id("Name"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");
    }
}