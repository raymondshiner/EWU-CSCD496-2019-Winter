using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        public IGroupService _GroupService { get; set; }

        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET api/Group/5
        [HttpGet("{groupId}")]
        public ActionResult<DTO.Group> GetGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            var group = _GroupService.GetGroup(groupId);
            return new DTO.Group(group);
        }

        // GET api/Group/5
        [HttpGet]
        public ActionResult<List<DTO.Group>> GetAllGroups()
        {
            var groupList = _GroupService.GetAllGroups();
            return groupList.Select(x => new DTO.Group(x)).ToList();
        }

        [HttpGet("user/{groupId}")]
        public ActionResult<List<DTO.User>> GetAllUsersInGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            var usersFromGroup = _GroupService.GetAllUsersInGroup(groupId);
            return usersFromGroup.Select(x => new DTO.User(x)).ToList();
        }

        // POST api/Group
        [HttpPost]
        public ActionResult<DTO.Group> AddGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            var domainGroup = DTO.Group.ToEntity(group);
            var finalGroup = _GroupService.AddGroup(domainGroup);

            return new DTO.Group(finalGroup);
        }

        // PUT api/Group/5
        [HttpPut]
        public ActionResult<DTO.Group> UpdateGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            var domainGroup = DTO.Group.ToEntity(group);
            var finalGroup = _GroupService.UpdateGroup(domainGroup);

            return new DTO.Group(finalGroup);
        }

        // DELETE api/Group/5
        [HttpDelete("{groupId}")]
        public ActionResult DeleteGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            _GroupService.DeleteGroup(groupId);
            return Ok();
        }

        // POST api/Group
        [HttpPost("{groupId}")]
        public ActionResult AddUserToGroup(int groupId, DTO.User user)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            if (user == null)
            {
                return BadRequest();
            }

            var domUser = DTO.User.ToEntity(user);
            _GroupService.AddUserToGroup(groupId, domUser);

            return Ok();
        }

        // DELETE api/Group/5
        [HttpDelete("{userId}")]
        public ActionResult RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            if (userId <= 0)
            {
                return NotFound();
            }

            _GroupService.RemoveUserFromGroup(userId, groupId);
            return Ok();
        }
    }
}
