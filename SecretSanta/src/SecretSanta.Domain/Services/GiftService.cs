using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        private ApplicationDbContext DbContext { get; }

        public GiftService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public void AddGiftToUser(Gift theGift, int userId)
        {
            if (theGift == null)
                return;

            User dbUser = DbContext.Users.Find(userId);

            if (dbUser == null)
                return;

            dbUser.Gifts.Add(theGift);
            theGift.User = dbUser;

            DbContext.Gifts.Add(theGift);

            DbContext.SaveChanges();
        }

        /*
        public void EditUserGift(Gift gift, User user)
        {
            if (gift == null || user == null)
                return;

            User dbUser = DbContext.Users.Find(user);

            if (dbUser == null)
                return;

            
            DbContext.SaveChanges();
        }
        */

        public bool RemoveUserGift(string giftId, int userId)
        {
            User dbUser = DbContext.Users.Find(userId);

            Gift theGift = DbContext.Gifts.Find(giftId);

            dbUser.Gifts.Remove(theGift);
            var check = DbContext.Gifts.Remove(theGift);

            bool res = check != null;

            DbContext.SaveChanges();

            return res;
        }
    }
}