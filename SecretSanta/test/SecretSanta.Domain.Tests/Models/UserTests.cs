using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Tests.Models
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

            //Act


            //Assert

        }

    }
}