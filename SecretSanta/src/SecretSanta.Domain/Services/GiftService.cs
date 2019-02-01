using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GiftService : IGiftService
    {
        private ApplicationDbContext DbContext { get; }

        public GiftService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Gift AddGiftToUser(int userId, Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            gift.UserId = userId;
            DbContext.Gifts.Add(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            gift.UserId = userId;
            DbContext.Gifts.Update(gift);
            DbContext.SaveChanges();

            return gift;
        }
        

        public void DeleteGiftFromUser(int giftId, int userId)
        {
            if (giftId <= 0 || userId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var user = DbContext.Users.Find(userId);
            var gift = DbContext.Gifts.Find(giftId);
            
            user.Gifts.Remove(gift);

            DbContext.SaveChanges();
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            return DbContext.Gifts.Where(g => g.UserId == userId).ToList();
        }

        public Gift AddGiftToUser(Gift gift, int userId)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            gift.UserId = userId;
            DbContext.Gifts.Add(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public void RemoveGift(Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            DbContext.Gifts.Remove(gift);
            DbContext.SaveChanges();
        }
    }
}