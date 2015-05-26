using System;

namespace modelInterface.exceptions
{
    public class UsernameExists : Exception
    {
        public string Username { get; private set; }
        
        public UsernameExists(string username)
        {
            Username = username;
        }
    }
}
