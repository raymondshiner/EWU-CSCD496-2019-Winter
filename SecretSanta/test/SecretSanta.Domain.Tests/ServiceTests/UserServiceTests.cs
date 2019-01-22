using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        private SqliteConnection Connection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        public User CreateUser()
        {
            var user = new User
            {
                FirstName = "Steve",
                LastName = "Franklin",
            };

            return user;
        }

        [TestInitialize]
        public void OpenConnection()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(Connection)
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void CloseConnection()
        {
            Connection.Close();
        }

        [TestMethod]
        public void AddUserToDatabase()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                User user = CreateUser();
                var service = new UserService(context);
                service.AddUser(user);

                Assert.AreNotEqual(0, user.Id);
            }
        }
        
        [TestMethod]
        public void FindUserReturnsAUser()
        {
            //Arrange
            using (var context = new ApplicationDbContext(Options))
            {
                User user = CreateUser();
                var service = new UserService(context);
                service.AddUser(user);
            }

            //Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                User user = service.FindUser(1);
                Assert.AreEqual("Steve", user.FirstName);
            }


            //Assert
        }

        [TestMethod]
        public void FindUserWithGiftReturnsAUserWithGift()
        {
            //Arrange
            using (var context = new ApplicationDbContext(Options))
            {
                User user = CreateUser();
                var service = new UserService(context);
                service.AddUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Gift theGift = new Gift();
                theGift.Title = "Something";
                var gService = new GiftService(context);

                gService.AddGiftToUser(theGift, 1);
            }
            
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                User user = service.FindUser(1);
                Assert.IsNotNull(user.Gifts);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            //Arrange
            using (var context = new ApplicationDbContext(Options))
            {
                User user = CreateUser();
                var service = new UserService(context);
                service.AddUser(user);
            }

            //Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                User user = service.FindUser(1);
                user.FirstName = "John";
                service.UpdateUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                User user = service.FindUser(1);
                Assert.AreEqual("John", user.FirstName);
            }
        }


        
    }
}