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

            DbContext.Gifts.Add(theGift);

            DbContext.SaveChanges();
        }

        public void UpdateGift(Gift theGift)
        {
            DbContext.Gifts.Update(theGift);
            DbContext.SaveChanges();
        }

        public void RemoveGift(int giftId)
        {
            Gift theGift = FindGift(giftId);
            DbContext.Gifts.Remove(theGift);
            DbContext.SaveChanges();
        }

        public Gift FindGift(int giftId)
        {
            return DbContext.Gifts.Find(giftId);
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