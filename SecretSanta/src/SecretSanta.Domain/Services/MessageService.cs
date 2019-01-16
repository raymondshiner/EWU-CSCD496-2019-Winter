using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class MessageService
    {
        private ApplicationDbContext DbContext { get; }

        public MessageService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public void AddMessage(Message msg)
        {
            DbContext.Messages.Add(msg);
            DbContext.SaveChanges();
        }

        //Create not required, we made it because we thought it might be useful later
        /* 
        public Message CreateMessage(int senderId, int recipientId, string text)
        {
            if (text == null)
                return null;

            User sender = DbContext.Users.Find(senderId);
            User recipient = DbContext.Users.Find(recipientId);

            if (sender == null || recipient == null)
                return null;

            Message msg = new Message();
            msg.Sender = sender;
            msg.Recipient = recipient;
            msg.Text = text;

            return msg;
        }
        */
    }
}