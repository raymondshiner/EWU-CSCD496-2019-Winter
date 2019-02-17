using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairingsController : ControllerBase
    {
        private IPairingService PairingService { get; set; }
        private IMapper Mapper { get; set; }

        public PairingsController(IPairingService service, IMapper mapper)
        {
            PairingService = service;
            Mapper = mapper;
        }

        // GET api/Pairing
        [HttpGet("groupId")]
        [Produces(typeof(ICollection<PairingViewModel>))]
        public async Task<IActionResult> Get(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            List<Pairing> pairingsList = await PairingService.GeneratePairings(groupId);

            if (pairingsList == null)
            {
                return BadRequest();
            }

            List<PairingViewModel> listToReturn = pairingsList.Select(x => Mapper.Map<PairingViewModel>(x)).ToList();
            return Ok(listToReturn);
        }
    }
}
