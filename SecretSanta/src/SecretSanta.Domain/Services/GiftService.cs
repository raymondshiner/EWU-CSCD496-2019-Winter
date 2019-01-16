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

            theGift.User = dbUser;
            dbUser.Gifts.Add(theGift);

            DbContext.Gifts.Add(theGift);

            DbContext.SaveChanges();
        }

        //Edit User Gift Goes Here

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


        //Create not required, we made it because we thought it might be useful later
        /*
        public Gift CreateGift(string title, int importance, string description, string url)
        {
            if (title == null || importance < 0)
                return null;

            Gift theGift = new Gift();

            theGift.Title = title;
            theGift.Importance = importance;
            theGift.Description = description;
            theGift.URL = url;

            return null;
        }
        */
    }
}