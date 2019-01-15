using System;
using System.Collections.Generic;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public List<User> Users { get; set; }

        public Group(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }
            Title = title;
            Users = new List<User>();
        }
    }
}