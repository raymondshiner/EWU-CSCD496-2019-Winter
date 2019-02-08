using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public int Find_Id { get; set; }
        public User Find_Return { get; set; }

        public User Find(int id)
        {
            Find_Id = id;
            return Find_Return;
        }

        public User AddUser_Return { get; set; }
        public User AddUser_User { get; set; }

        public User AddUser(User user)
        {
            AddUser_User = user;
            return AddUser_Return;
        }

        public int DeleteUser_UserId { get; set; }
        public bool DeleteUser_Return{ get; set; }
        public bool DeleteUser(int userId)
        {
            DeleteUser_UserId = userId;
            return DeleteUser_Return;
        }

        public List<User> FetchAll_Return { get; set; }

        public List<User> FetchAll()
        {
            return FetchAll_Return;
        }

        public User UpdateUser_Return { get; set; }
        public User UpdateUser_User { get; set; }

        public User UpdateUser(User user)
        {
            UpdateUser_User = user;
            return UpdateUser_Return;
        }
    }
}
