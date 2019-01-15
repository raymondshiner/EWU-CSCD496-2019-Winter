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
            if (group == null || user == null)
                return;

            Group dbGroup = DbContext.Groups.Find(group);

            if(dbGroup == null)
                return;

            dbGroup.Users.Add(user); // add user to group db
            user.Groups.Add(dbGroup);// add group to specific user ref passed in

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