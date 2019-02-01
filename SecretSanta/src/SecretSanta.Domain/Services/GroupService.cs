using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Domain.Services
{
    public class GroupService : IGroupService
    {
        private ApplicationDbContext DbContext { get; }

        public GroupService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Group GetGroup(int groupId)
        {
            return DbContext.Groups.Find(groupId);
        }

        public Group AddGroup(Group @group)
        {
            DbContext.Groups.Add(@group);
            DbContext.SaveChanges();
            return @group;
        }

        public Group UpdateGroup(Group @group)
        {
            DbContext.Groups.Update(@group);
            DbContext.SaveChanges();
            return @group;
        }

        public void DeleteGroup(int groupId)
        {
            var group = DbContext.Groups.Find(groupId);
            DbContext.Remove(group);
            DbContext.SaveChanges();
        }

        public void AddUserToGroup(int groupId, User user)
        {
            var group = DbContext.Groups.Find(groupId);
            var groupUser = new GroupUser
            {
                UserId = user.Id,
                GroupId = group.Id
            };

            group.GroupUsers = new List<GroupUser> { groupUser };
            
            DbContext.SaveChanges();
        }

        public void RemoveUserFromGroup(int groupId, int userId)
        {
            var group = DbContext.Groups.Find(groupId);
            var groupUser = new GroupUser
            {
                UserId = userId,
                GroupId = group.Id
            };

            group.GroupUsers.Remove(groupUser);

            DbContext.SaveChanges();
        }

        public List<Group> FetchAll()
        {
            return DbContext.Groups.ToList();
        }

        public List<User> GetUsers(int groupId)
        {
            return DbContext.Groups
                .Where(x => x.Id == groupId)
                .SelectMany(x => x.GroupUsers)
                .Select(x => x.User)
                .ToList();
        }
    }
}