using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        public async Task GeneratePairings_InvalidGroupId_ReturnsNull()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                
                var result = await service.GeneratePairings(-1);

                Assert.IsNull(result);
            }
        }


        [TestMethod]
        public async Task GeneratePairings_GroupSize1_ReturnsNull()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var groupService = new GroupService(context);
                var group = new Group
                {
                    Name = "Group 1"
                };
                await groupService.AddGroup(group);

                var user = new User()
                {
                    FirstName = "Ray",
                    LastName = "Shiner"
                };

                var userService = new UserService(context);

                await userService.AddUser(user);

                await groupService.AddUserToGroup(1, 1);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);

                var result = await service.GeneratePairings(1);

                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_ValidData_PairingsAreAddedAndRandomized()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var groupService = new GroupService(context);
                var group = new Group
                {
                    Name = "Group 1"
                };
                await groupService.AddGroup(group);

                var user1 = new User()
                {
                    FirstName = "Ray",
                    LastName = "Shiner"
                };

                var user2 = new User()
                {
                    FirstName = "Frank",
                    LastName = "Bobbin"
                };

                var user3 = new User()
                {
                    FirstName = "Tommy",
                    LastName = "Mills"
                };

                var user4 = new User()
                {
                    FirstName = "Louis",
                    LastName = "Hilton"
                };

                var userService = new UserService(context);

                await userService.AddUser(user1);
                await userService.AddUser(user2);
                await userService.AddUser(user3);
                await userService.AddUser(user4);

                await groupService.AddUserToGroup(1, 1);
                await groupService.AddUserToGroup(1, 2);
                await groupService.AddUserToGroup(1, 3);
                await groupService.AddUserToGroup(1, 4);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);

                var resultPairings = await service.GeneratePairings(1);

                var pairingList = new List<Pairing>()
                {
                    new Pairing {Id = 1, SantaId = 1, RecipientId = 2},
                    new Pairing {Id = 2, SantaId = 2, RecipientId = 3},
                    new Pairing {Id = 3, SantaId = 3, RecipientId = 4},
                    new Pairing {Id = 4, SantaId = 4, RecipientId = 1},
                };

                Assert.IsTrue(resultPairings.Count == 4);
                Assert.AreNotEqual(pairingList, resultPairings);
            }
        }
    }
}
