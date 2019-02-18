using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class PairingControllerTests
    {
        [TestMethod]
        public async Task GeneratePairings_InvalidGroupId_ReturnsBadRequest()
        {
            var service = new TestablePairingService();
            var controller = new PairingsController(service, Mapper.Instance);

            var result = await controller.GeneratePairings(-1);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public async Task GeneratePairings_PairingsListIsNull_ReturnsBadRequest()
        {
            var service = new TestablePairingService();
            service.GeneratePairings_ToReturn = null;
            var controller = new PairingsController(service, Mapper.Instance);

            var result = await controller.GeneratePairings(1);

            Assert.AreEqual(1, service.GeneratePairings_GroupId);
            Assert.IsNull(service.GeneratePairings_ToReturn);
            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public async Task GeneratePairings_ValidData_GeneratePairingsIsCalled()
        {
            var service = new TestablePairingService();
            service.GeneratePairings_ToReturn = new List<Pairing>
            {
                new Pairing
                {
                    Id = 4
                }
            };

            var controller = new PairingsController(service, Mapper.Instance);

            var result = await controller.GeneratePairings(1);

            Assert.AreEqual(1, service.GeneratePairings_GroupId);
            Assert.IsTrue(result is OkObjectResult);
        }


    }
}
