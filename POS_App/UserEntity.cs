using System;

namespace POS_App
{
    public abstract class UserEntity
    {
        // Properties

        public string Name { get; set; }
        public string Email { get; set; }
        private string Password { get; set; }

        // Constructor

        protected UserEntity(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        // Methods

        public bool Login(string email, string password)
        {
            return Email == email && Password == password;
        }
    }
}