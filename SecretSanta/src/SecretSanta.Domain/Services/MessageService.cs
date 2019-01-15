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

        public void AddMessage(int senderId, int recipientId, string text)
        {
            if (text == null)
                return;

            User sender = DbContext.Users.Find(senderId);
            User recipient = DbContext.Users.Find(recipientId);

            if(sender == null || recipient == null)
                return;
            
            Message msg = new Message();
            msg.Sender = sender;
            msg.Recipient = recipient;
            msg.Text = text;

            DbContext.Messages.Add(msg);
            DbContext.SaveChanges();
        }
    }
}