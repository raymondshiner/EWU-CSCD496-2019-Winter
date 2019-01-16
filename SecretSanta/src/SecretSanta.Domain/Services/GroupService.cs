using System;
using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GroupService
    {
        private ApplicationDbContext DbContext { get; }

        public GroupService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Group CreateGroup(string title)
        {
            Group group = new Group(title);
            group.Users = new List<User>();
            return group;
        }

        public void AddGroup(Group group)
        {
            DbContext.Groups.Add(group);
            DbContext.SaveChanges();
        }

        public void AddUserToGroup(int userId, string groupName)
        {
            if (groupName == null)
                return;

            Group dbGroup = DbContext.Groups.Find(groupName);
            User user = DbContext.Users.Find(userId);

            if (dbGroup == null)
                throw new ArgumentException("Cannot add a User to a null Group.");
            if (user == null)
                return;

            dbGroup.Users.Add(user);
            user.Groups.Add(dbGroup);

            DbContext.SaveChanges();
        }

        public bool RemoveUserFromGroup(int userId, string groupName)
        {
            if (groupName == null)
                return false;

            Group dbGroup = DbContext.Groups.Find(groupName);

            if (dbGroup == null)
                return false;

            User user = DbContext.Users.Find(userId);

            bool userFromGroup = dbGroup.Users.Remove(user);
            DbContext.SaveChanges();

            return userFromGroup;
        }

    }
}