using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            DbContext.Pairings.Add(pairing);
            DbContext.SaveChanges();
        }

        public Pairing FindPairing(int pairingId)
        {
            return DbContext.Pairings.Include(p => p.Santa).Include(p => p.Recipient).SingleOrDefault();
        }

        //Create not required, we made it because we thought it might be useful later
        /*
        public Pairing CreatePairing(int recipientId, string santaId)
        {
            User recipient = DbContext.Users.FindGift(recipientId);
            User santa = DbContext.Users.FindGift(santaId);

            if (recipient == null || santa == null)
                return null;

            Pairing pairing = new Pairing();

            pairing.Recipient = recipient;
            pairing.Santa = santa;

            return pairing;
        }
        */
    }
}