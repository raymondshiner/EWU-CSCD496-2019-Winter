using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.ModelTests
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void DefaultConstructor_returnsMessageObject()
        {
            Message message = new Message();
            Assert.IsTrue(message is Message);
            Assert.IsTrue(message is Entity);
        }

        [TestMethod]
        public void SetAndGetSender()
        {
            Message message = new Message();
            User newSender = new User();
            message.Sender = newSender;
            Assert.AreEqual(newSender, message.Sender);
        }

        [TestMethod]
        public void SetAndGetRecipient()
        {
            Message message = new Message();
            User newRecipient = new User();
            message.Sender = newRecipient;
            Assert.AreEqual(newRecipient, message.Sender);
        }

        [TestMethod]
        public void SetAndGetText()
        {
            Message message = new Message();
            string expected = "Burritos are deliciuos.";
            message.Text = expected;
            Assert.AreEqual(expected, message.Text);
        }
    }
}
