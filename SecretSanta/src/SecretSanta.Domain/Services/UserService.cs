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

        public void AddUser(User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            User jdoe = DbContext.Users.Find(user);

            if (jdoe == null)
                return;

            if(user.FirstName != null)
                jdoe.FirstName = user.FirstName;

            if (user.LastName != null)
                jdoe.LastName = user.LastName;

            if (user.Id != null)
                jdoe.Id = user.Id;

            DbContext.SaveChanges();
        }
    }
}
