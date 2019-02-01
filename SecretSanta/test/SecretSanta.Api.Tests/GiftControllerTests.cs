using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftController_RequiresGiftService()
        {
            new GiftController(null);
        }

        [TestMethod]
        public void GetAllGiftForUser_ReturnsUsersFromService()
        {
            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                OrderOfImportance = 1
            };
            var testService = new TestableGiftService
            {
                GetAllGiftsForUser_Return =  new List<Gift>
                {
                    gift
                }
            };
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.GetAllGiftsForUser(4);

            Assert.AreEqual(4, testService.GetAllGiftsForUser_UserId);
            DTO.Gift resultGift = result.Value.Single();
            Assert.AreEqual(gift.Id, resultGift.Id);
            Assert.AreEqual(gift.Title, resultGift.Title);
            Assert.AreEqual(gift.Description, resultGift.Description);
            Assert.AreEqual(gift.Url, resultGift.Url);
            Assert.AreEqual(gift.OrderOfImportance, resultGift.OrderOfImportance);
        }

        [TestMethod]
        public void GetAllGiftForUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.GetAllGiftsForUser(-1);
            
            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetAllGiftsForUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.AddGiftToUser(null, 4);

            Assert.IsTrue(result is BadRequestResult);
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_InvokesService()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var giftDTO = new DTO.Gift();

            ActionResult result = controller.AddGiftToUser(giftDTO, 4);

            var okResult = result as OkResult;

            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual<int>(4, testService.AddGiftToUser_UserId);

            Assert.AreEqual(giftDTO.Id, testService.AddGiftToUser_Gift.Id);
            Assert.AreEqual(giftDTO.Description, testService.AddGiftToUser_Gift.Description);
            Assert.AreEqual(giftDTO.Title, testService.AddGiftToUser_Gift.Title);
            Assert.AreEqual(giftDTO.OrderOfImportance, testService.AddGiftToUser_Gift.OrderOfImportance);
            Assert.AreEqual(giftDTO.Url, testService.AddGiftToUser_Gift.Url);

        }

        [TestMethod]
        public void UpdateGiftForUser_UpdatesGift()
        {
            var giftToReturn = new Gift
            {
                Id = 2,
                Title = "Xbox",
                Description = "It's Cool"
            };

            var testService = new TestableGiftService 
            {
                UpdateGiftForUser_Return = giftToReturn
            };

            var controller = new GiftController(testService);

            var giftPassedIn = new Gift
            {
                Id = 4,
                Title = "new title",
                Description = "new description"
            };

            var resultGift = controller.UpdateGiftForUser(4, new DTO.Gift(giftPassedIn));

            Assert.AreEqual<int>(giftToReturn.Id, resultGift.Value.Id);
            Assert.AreEqual<string>(giftToReturn.Title, resultGift.Value.Title);
            Assert.AreEqual<string>(giftToReturn.Description, resultGift.Value.Description);

            Assert.AreEqual<int>(giftPassedIn.Id, testService.UpdateGiftForUser_Gift.Id);
            Assert.AreEqual<string>(giftPassedIn.Title, testService.UpdateGiftForUser_Gift.Title);
            Assert.AreEqual<string>(giftPassedIn.Description, testService.UpdateGiftForUser_Gift.Description);

            Assert.AreEqual<int>(4, testService.UpdateGiftForUser_UserId);
        }

        [TestMethod]
        public void UpdateGiftForUser_NegativeUserIdReturnsNotFoundResult()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            int giftId = -5;

            var result = controller.UpdateGiftForUser(giftId, new DTO.Gift());

            Assert.IsTrue(result.Result is NotFoundResult);
            Assert.AreEqual(0, testService.UpdateGiftForUser_UserId);
            Assert.IsNull(testService.UpdateGiftForUser_Gift);
        }

        [TestMethod]
        public void UpdateGiftForUser_NullGiftReturnsBadRequestResult()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            int giftId = 5;

            var result = controller.UpdateGiftForUser(giftId, null);

            Assert.IsTrue(result.Result is BadRequestResult);
            Assert.AreEqual(0, testService.UpdateGiftForUser_UserId);
            Assert.IsNull(testService.UpdateGiftForUser_Gift);
        }

        [TestMethod]
        public void DeleteGiftFromUser_DeletesGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            int giftId = 5;
            int userId = 3;

            var result = controller.DeleteGiftFromUser(giftId, userId);

            Assert.IsTrue(result is OkResult);
            Assert.AreEqual<int>(5, testService.DeleteGiftFromUser_GiftId);
            Assert.AreEqual<int>(3, testService.DeleteGiftFromUser_UserId);
        }

        [TestMethod]
        public void DeleteGiftFromUser_NegativeUserIdReturnsNotFoundResult()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            int giftId = 5;
            int userId = -3;

            var result = controller.DeleteGiftFromUser(giftId, userId);

            Assert.IsTrue(result is NotFoundResult);
            Assert.AreEqual<int>(0, testService.DeleteGiftFromUser_GiftId);
            Assert.AreEqual<int>(0, testService.DeleteGiftFromUser_UserId);
        }

        [TestMethod]
        public void DeleteGiftFromUser_NegativeGiftIdReturnsNotFoundResult()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            int giftId = -5;
            int userId = 3;

            var result = controller.DeleteGiftFromUser(giftId, userId);

            Assert.IsTrue(result is NotFoundResult);
            Assert.AreEqual<int>(0, testService.DeleteGiftFromUser_GiftId);
            Assert.AreEqual<int>(0, testService.DeleteGiftFromUser_UserId);
        }

    }
}
