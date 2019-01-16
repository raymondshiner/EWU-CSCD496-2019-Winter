using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddUser(User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
        }

        public User FindUser(int userId)
        {
            return DbContext.Users.Find(userId);
        }

        public void UpdateUser(User user)
        {
            DbContext.Users.Update(user);
            DbContext.SaveChanges();
        }

        //Create not required, we made it because we thought it might be useful later
        /*
        public User CreateUser(string firstname, string lastname)
        {
            if (firstname == null || lastname == null)
                return null;

            User user = new User();

            user.FirstName = firstname;
            user.LastName = lastname;

            return user;
        }
        */
    }
}
