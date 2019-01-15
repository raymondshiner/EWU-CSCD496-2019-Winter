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

            //Act
            


            //Assert

        }

        [TestMethod]
        public void UserService_AddUser()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            UserService userService = new UserService(dbContext);

            User u4 = new User();

            u4.FirstName = "Steve";

            userService.AddUser(u4);

            Assert.AreEqual(dbContext.Users.Find(u4), u4);
        }

    }
}