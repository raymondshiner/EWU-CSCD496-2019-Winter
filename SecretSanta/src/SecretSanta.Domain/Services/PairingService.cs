using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        private ApplicationDbContext DbContext { get; }

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Pairing>> GeneratePairings(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();
            if (userIds == null || userIds.Count == 2)
            {
                return null;
            }

            Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds));
            var myPairings = await task;

            if (myPairings == null)
            {
                return null;
            }

            await DbContext.AddRangeAsync(myPairings);
            await DbContext.SaveChangesAsync();

            return myPairings;
        }

        private List<Pairing> GetPairings(List<int> userIds)
        {
            var pairings = new List<Pairing>();

            for (int x = 0; x < userIds.Count - 1; x++)
            {
                var pairing = new Pairing
                {
                    SantaId = userIds[x],
                    RecipientId = userIds[x + 1]
                };
                pairings.Add(pairing);
            }

            var finalPairing = new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First()
            };
            pairings.Add(finalPairing);

            var random = new Random();
            int n = pairings.Count;
            while (n > 1)
            {
                n--;
                int randomIndex = random.Next(n + 1);
                Pairing temp = pairings[randomIndex];
                pairings[randomIndex] = pairings[n];
                pairings[n] = temp;
            }

            return pairings;
        }
    }
}