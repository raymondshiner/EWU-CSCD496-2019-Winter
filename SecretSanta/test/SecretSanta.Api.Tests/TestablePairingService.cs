using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Api.Tests
{
    public class TestablePairingService : IPairingService
    {
        public List<Pairing> GeneratePairings_ToReturn { get; set; }
        public int GeneratePairings_GroupId { get; set; }

        public Task<List<Pairing>> GeneratePairings(int groupId)
        {
            GeneratePairings_GroupId = groupId;
            Task<List<Pairing>> returnTask = Task.FromResult(GeneratePairings_ToReturn);
            return returnTask;
        }
    }
}
