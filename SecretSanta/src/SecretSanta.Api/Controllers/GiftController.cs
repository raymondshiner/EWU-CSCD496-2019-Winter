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
        public ActionResult<List<DTO.Gift>> GetAllGiftsForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = _GiftService.GetAllGiftsForUser(userId);

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

            var domainGift = DTO.Gift.ToEntity(gift);
            _GiftService.AddGiftToUser(domainGift, userId);

            return Ok();
        }

        [HttpPut("{userId}")]
        public ActionResult<DTO.Gift> UpdateGiftForUser(int userId, DTO.Gift gift)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            if (gift == null)
            {
                return BadRequest();
            }

            var domainGift = DTO.Gift.ToEntity(gift);
            var finalGift = _GiftService.UpdateGiftForUser(userId, domainGift);

            return new DTO.Gift(finalGift);
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteGiftFromUser(int giftId, int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            if (giftId <= 0)
            {
                return NotFound();
            }

            _GiftService.DeleteGiftFromUser(giftId, userId);

            return Ok();
        }
    }
}
