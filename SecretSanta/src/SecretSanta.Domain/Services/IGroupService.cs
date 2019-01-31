using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        Group GetGroup(int groupId);
        Group AddGroup(Group @group);
        Group UpdateGroup(Group @group);
        void DeleteGroup(int groupId);

        void AddUserToGroup(int groupId, User user);

        void RemoveUserFromGroup(int groupId, int userId);
    }
}
