using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.ServiceTests
{
    [TestClass]
    public class MessageServiceTests
    {
        private SqliteConnection Connection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }
        public Message Message { get; set; }

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

            CreateTestMessage();
        }

        private void CreateTestMessage()
        {
            Message = new Message()
            {
                Sender = CreateTestSender(),
                Recipient = CreateTestRecipient(),
                Text = "Cats are awesome!"
            };
        }

        private User CreateTestSender()
        {
            return new User
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };
        }

        private User CreateTestRecipient()
        {
            return new User
            {
                FirstName = "Bob",
                LastName = "McPerson"
            };
        }

        [TestCleanup]
        public void CloseConnection()
        {
            Connection.Close();
        }

        [TestMethod]
        public void AddMessage_ValidMessage_MessageAdded()
        {
            var context = new ApplicationDbContext(Options);
            var service = new MessageService(context);
            service.AddMessage(Message);

            Message retrievedMessage = context.Messages.Find(Message.Id);

            Assert.IsNotNull(retrievedMessage);
            Assert.AreEqual("Cats are awesome!", retrievedMessage.Text);
            Assert.AreEqual("Inigo", retrievedMessage.Sender.FirstName);
            Assert.AreEqual("Montoya", retrievedMessage.Sender.LastName);
            Assert.AreEqual("Bob", retrievedMessage.Recipient.FirstName);
            Assert.AreEqual("McPerson", retrievedMessage.Recipient.LastName);
        }
    }
}
