using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.ModelTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser()
        {
            //Arrange

            User user = new User {FirstName = "Inigo", LastName = "Montoya"};
            Assert.AreEqual("Inigo", user.FirstName);

        }
    }
}