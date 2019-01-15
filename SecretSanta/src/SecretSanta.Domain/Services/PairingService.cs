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

        public void AddPairing(Pairing pairing)
        {
            if (pairing?.Recipient == null || pairing.Santa == null) 
                return;

            DbContext.Pairings.Add(pairing);
            DbContext.SaveChanges();
        }
    }
}