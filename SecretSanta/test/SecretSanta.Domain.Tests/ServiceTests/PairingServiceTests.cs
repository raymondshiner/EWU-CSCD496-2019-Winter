using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.ServiceTests
{
    [TestClass]
    public class PairingServiceTests
    {
        private SqliteConnection Connection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        public User CreateUser1()
        {
            var user = new User
            {
                FirstName = "Steve",
                LastName = "Franklin",
            };

            return user;
        }

        public User CreateUser2()
        {
            var user = new User
            {
                FirstName = "Terry",
                LastName = "Crews",
            };

            return user;
        }

        public Pairing CreatePairing(User santa, User recipient)
        {
            var pairing = new Pairing
            {
                Santa = santa,
                Recipient = recipient
            };

            return pairing;
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
        public void AddPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                User santa = CreateUser1();
                User recipient = CreateUser2();

                var pairing = CreatePairing(santa, recipient);

                Assert.AreEqual(0, pairing.Id);

                service.AddPairing(pairing);

                Assert.AreEqual(1, pairing.Id);
            }
        }

        [TestMethod]
        public void FindPairing_ReturnsPairingWithSantaAndRecipient()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                User santa = CreateUser1();
                User recipient = CreateUser2();

                var pairing = CreatePairing(santa, recipient);

                service.AddPairing(pairing);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var pairing = service.FindPairing(1);

                Assert.AreEqual<string>("Steve", pairing.Santa.FirstName);
                Assert.AreEqual<string>("Terry", pairing.Recipient.FirstName);
            }
        }

    }
}