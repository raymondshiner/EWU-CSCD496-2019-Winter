using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Tests
{
    class TestableUserService : IUserService
    {
        public int GetUser_UserId { get; set; }
        public User GetUser_Return { get; set; }

        public User GetUser(int userId)
        {
            GetUser_UserId = userId;
            return GetUser_Return;
        }

        public User AddUser_User { get; set; }
        public User AddUser_Return { get; set; }


        public User AddUser(User user)
        {
            AddUser_User = user;
            return AddUser_Return;
        }

        public User UpdateUser_User { get; set; }
        public User UpdateUser_Return { get; set; }

        public User UpdateUser(User user)
        {
            UpdateUser_User = user;
            return UpdateUser_Return;
        }

        public int DeleteUser_UserId { get; set; }

        public void DeleteUser(int userId)
        {
            DeleteUser_UserId = userId;
        }
    }
}
