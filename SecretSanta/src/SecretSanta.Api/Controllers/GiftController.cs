using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _GiftService;

        public GiftController(IGiftService giftService)
        {
            _GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }
        
        [HttpPost("{userId}")]
        public ActionResult AddGiftToUser(DTO.Gift gift, int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            if (gift == null)
            {
                return BadRequest();
            }

            var newGift = new Gift
            {
                Url = gift.Url,
                Id = gift.Id,
                Description = gift.Description,
                Title = gift.Title,
                OrderOfImportance = gift.OrderOfImportance
            };

            _GiftService.AddGiftToUser(newGift, userId);

            return Ok();
        }
    }
}
