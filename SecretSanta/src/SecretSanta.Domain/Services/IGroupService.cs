using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        List<Group> GetAllGroups();

        Group GetGroup(int groupId);
        Group AddGroup(Group @group);
        Group UpdateGroup(Group @group);
        void DeleteGroup(int groupId);

        void AddUserToGroup(int groupId, User user);

        void RemoveUserFromGroup(int groupId, int userId);
        List<User> GetAllUsersInGroup(int groupId);
    }
}
