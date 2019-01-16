using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class PairingService
    {
        private ApplicationDbContext DbContext { get; }

        public PairingService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Pairing CreatePairing(int recipientId, string santaId)
        {
            User recipient = DbContext.Users.Find(recipientId);
            User santa = DbContext.Users.Find(santaId);

            if (recipient == null || santa == null)
                return null;

            Pairing pairing = new Pairing();

            pairing.Recipient = recipient;
            pairing.Santa = santa;

            return pairing;
        }

        public void AddPairing(Pairing pairing)
        {
            DbContext.Pairings.Add(pairing);
            DbContext.SaveChanges();
        }
    }
}