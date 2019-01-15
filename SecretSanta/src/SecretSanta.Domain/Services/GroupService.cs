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
            DbContext.Groups.Add(group);
            DbContext.SaveChanges();
            return group;
        }

        public void AddUserToGroup(Group group, User user)
        {
            if (group is null)
            {
                throw new ArgumentException("Cannot add a User to a null Group.");
            }
            if (user is null)
            {
                throw new ArgumentException("Cannot add a null User.");
            }

            Group dbGroup = DbContext.Groups.Find(group);

            if (dbGroup is null)
            {
                throw new Exception("Could not find specified group in DbContext.");
            }

            dbGroup.Users.Add(user);
            user.Groups.Add(dbGroup);

            DbContext.SaveChanges();
        }

        public bool RemoveUserFromGroup(Group group, User user)
        {
            if (group == null || user == null)
                return false;

            Group dbGroup = DbContext.Groups.Find(group);

            if (dbGroup == null)
                return false;

            bool userFromGroup = dbGroup.Users.Remove(user);
            DbContext.SaveChanges();

            return userFromGroup;
        }

    }
}