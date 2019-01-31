using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Tests
{
    class TestableGroupService : IGroupService
    {
        public Group GetGroup_Return { get; set; }
        public int GetGroup_GroupId { get; set; }

        public Group GetGroup(int groupId)
        {
            GetGroup_GroupId = groupId;
            return GetGroup_Return;
        }

        public Group AddGroup_Group { get; set; }
        public Group AddGroup_Return { get; set; }

        public Group AddGroup(Group @group)
        {
            AddGroup_Group = group;
            return AddGroup_Return;
        }

        public Group UpdateGroup_Group { get; set; }
        public Group UpdateGroup_Return { get; set; }

        public Group UpdateGroup(Group @group)
        {
            UpdateGroup_Group = group;
            return UpdateGroup_Return;
        }

        public int DeleteGroup_GroupId { get; set; }

        public void DeleteGroup(int groupId)
        {
            DeleteGroup_GroupId = groupId;
        }
    }
}
