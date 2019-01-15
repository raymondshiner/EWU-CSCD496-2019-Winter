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
            if (msg == null)
                return;

            DbContext.Messages.Add(msg);
            DbContext.SaveChanges();
        }
    }
}