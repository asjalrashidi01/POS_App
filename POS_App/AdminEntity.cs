using System;

namespace POS_App
{
    public class AdminEntity : UserEntity
    {
        // Constructor

        public AdminEntity(string name, string email, string password) : base(name, email, password)
        {
        }
    }
}