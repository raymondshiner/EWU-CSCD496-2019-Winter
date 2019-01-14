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
    }
}
