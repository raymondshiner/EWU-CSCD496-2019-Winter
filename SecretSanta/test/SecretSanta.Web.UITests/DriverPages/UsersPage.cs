﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.DriverPages
{
    public class UsersPage
    {
        public const string Path = HomePage.Path + "Users/";
        public const string Slug = "Users";
        public IWebDriver Driver { get; }
        public UsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public IWebElement AddUserLink => Driver.FindElement(By.LinkText("Add User"));

        /// <summary>
        /// Returns a list of all the usernames of user objects listed on the users page in the form of "FirstName LastName"
        /// </summary>
        public List<string> UserNames
        {
            get
            {
                var elements = Driver.FindElements(By.CssSelector("h1+ul>li"));

                return elements.Select(x =>
                {
                    var text = x.Text;
                    if (text.EndsWith(" Edit Delete"))
                    {
                        text = text.Substring(0, text.Length - " Edit Delete".Length);
                    }

                    return text;
                }).ToList();
            }
        }

        public IWebElement GetDeleteLink(string userName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks =
                Driver.FindElements(By.CssSelector("a.is-danger"));

            return deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{userName}?')"));
        }

        public IWebElement GetEditLink(string userName)
        {
            ReadOnlyCollection<IWebElement> editLinks =
                Driver.FindElements(By.CssSelector("h1+ul>li"));

            return editLinks.Single(x => x.Text.StartsWith(userName)).FindElement(By.CssSelector("a.button"));
        }

        public IWebElement GetNavbarLink(string linkName) => Driver.FindElement(By.LinkText(linkName));
    }

}