using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.ServiceTests
{
    [TestClass]
    public class GiftServiceTests
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

        public Gift CreateGift()
        {
            var gift = new Gift
            {
                Title = "Xbox",
                Importance = 1,
                Description = "Xboxes are cool",
                URL = "https://www.amazon.com/xbox"
            };

            return gift;
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
        public void AddGiftToUser()
        {
            User user;
            using (var context = new ApplicationDbContext(Options))
            {
                user = CreateUser();
                var service = new UserService(context);
                service.AddUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Gift gift = CreateGift();
                var service = new GiftService(context);
                service.AddGiftToUser(gift, 1);
                
                Assert.AreEqual("Steve", gift.User.FirstName);
               // Assert.IsTrue(user.Gifts.Contains(gift));
            }
        }

        [TestMethod]
        public void UpdateGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                User user = CreateUser();
                var service = new UserService(context);
                service.AddUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Gift gift = CreateGift();
                var service = new GiftService(context);
                service.AddGiftToUser(gift, 1);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                Gift theGift = service.FindGift(1);
                theGift.Title = "PS4";
                service.UpdateGift(theGift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                Gift theGift = service.FindGift(1);
                Assert.AreEqual("PS4", theGift.Title);
            }


        }

        [TestMethod]
        public void RemoveGift()
        {
            User user;
            using (var context = new ApplicationDbContext(Options))
            {
                user = CreateUser();
                var service = new UserService(context);
                service.AddUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Gift gift = CreateGift();
                var service = new GiftService(context);
                service.AddGiftToUser(gift, 1);

                Assert.IsTrue(context.Gifts.Contains(gift));

                service.RemoveGift(gift.Id);

                Assert.IsFalse(context.Gifts.Contains(gift));
            }
        }

        [TestMethod]
        public void FindGiftReturnsAGift()
        {
            //Arrange
            using (var context = new ApplicationDbContext(Options))
            {
                User user = CreateUser();
                var uService = new UserService(context);
                uService.AddUser(user);

                Gift gift = CreateGift();
                var gService = new GiftService(context);
                gService.AddGiftToUser(gift, 1);
            }

            //Act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                Gift gift = service.FindGift(1);
                Assert.AreEqual("Xbox", gift.Title);
            }
        }




    }
}