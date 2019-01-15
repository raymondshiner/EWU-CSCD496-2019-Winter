using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class UserService
    {
        private ApplicationDbContext DbContext { get; }

        public UserService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public void AddUser(string firstname, string lastname)
        {
            if (firstname == null || lastname == null)
                return;

            User user = new User();
            
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Groups = new List<Group>();
            user.Gifts = new List<Gift>();

            DbContext.Users.Add(user);
            DbContext.SaveChanges();
        }

        public void UpdateUserName(int userId,string firstname, string lastname)
        {
            User jdoe = DbContext.Users.Find(userId);

            if (jdoe == null)
                return;

            if(firstname != null)
                jdoe.FirstName = firstname;

            if (lastname != null)
                jdoe.LastName = lastname;

            DbContext.SaveChanges();
        }
    }
}
