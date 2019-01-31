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
    }
}
