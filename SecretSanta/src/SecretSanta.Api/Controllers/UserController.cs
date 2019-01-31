using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET api/User
        [HttpGet("{userId}")]
        public ActionResult<DTO.User> GetUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            var user = _UserService.GetUser(userId);
            return new DTO.User(user);
        }

        // POST api/User
        [HttpPost]
        public ActionResult<DTO.User> AddUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var domainUser = DTO.User.ToEntity(user);

            var result = _UserService.AddUser(domainUser);

            return new DTO.User(result);
        }

        // PUT api/User/5
        [HttpPut("{userId}")]
        public ActionResult<DTO.User> UpdateUser(int userId, DTO.User user)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            if (user == null)
            {
                return BadRequest();
            }

            var domainUser = DTO.User.ToEntity(user);
            domainUser.Id = userId;

            var result = _UserService.UpdateUser(domainUser);

            return new DTO.User(result);
        }

        // DELETE api/User/5
        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            _UserService.DeleteUser(userId);

            return Ok();
        }
    }
}
