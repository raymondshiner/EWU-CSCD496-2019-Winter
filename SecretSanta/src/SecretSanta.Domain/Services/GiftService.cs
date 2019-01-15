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

        public void AddGiftToUser(Gift gift, User user)
        {
            if (gift == null || user == null)
                return;

            User dbUser = DbContext.Users.Find(user);

            if (dbUser == null)
                return;

            dbUser.Gifts.Add(gift);
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

        public bool RemoveUserGift(Gift gift, User user)
        {
            if (gift == null || user == null)
                return false;

            User dbUser = DbContext.Users.Find(user);
            bool res = dbUser.Gifts.Remove(gift);
            DbContext.SaveChanges();
            return res;
        }
    }
}