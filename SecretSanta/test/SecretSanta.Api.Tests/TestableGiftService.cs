using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public List<Gift> GetAllGiftsForUser_Return { get; set; }
        public int GetAllGiftsForUser_UserId { get; set; }

        public List<Gift> GetAllGiftsForUser(int userId)
        {
            GetAllGiftsForUser_UserId = userId;
            return GetAllGiftsForUser_Return;
        }


        public Gift AddGiftToUser_Return { get; set; }

        public Gift AddGiftToUser_Gift { get; set; }
        public int AddGiftToUser_UserId { get; set; }
        
        public Gift AddGiftToUser(Gift gift, int userId)
        {
            AddGiftToUser_UserId = userId;
            AddGiftToUser_Gift = gift;

            return AddGiftToUser_Return;
        }

        public Gift UpdateGiftForUser_Gift { get; set; }
        public int UpdateGiftForUser_UserId { get; set; }
        public Gift UpdateGiftForUser_Return { get; set; }

        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            UpdateGiftForUser_Gift = gift;
            UpdateGiftForUser_UserId = userId;

            return UpdateGiftForUser_Return;
        }

        public int DeleteGiftFromUser_UserId { get; set; }
        public int DeleteGiftFromUser_GiftId { get; set; }
        
        public void DeleteGiftFromUser(int giftId, int userId)
        {
            DeleteGiftFromUser_GiftId = giftId;
            DeleteGiftFromUser_UserId = userId;
        }
    }
}