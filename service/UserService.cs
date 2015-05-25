using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;
using model.model;

namespace service
{
    class UserService
    {
        private readonly Context _context;


        public UserService(Context context)
        {
            _context = context;
        }

        public User GetUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return null;
            if (!HashMatchesPassword(password, user.Hash)) return null;
            return user;
        }

        public Token GetToken(Guid guid)
        {
            return _context.Tokens.FirstOrDefault(t => t.Guid == guid);
        }

        public Token CreateToken(string username, string password)
        {
            var user = GetUser(username, password);
            return CreateToken(user);
        }

        private Token CreateToken(User user)
        {
            var guid = Guid.NewGuid();
            while (_context.Tokens.Any(t => t.Guid == guid))
            {
                guid = Guid.NewGuid();
            }
            var token = new Token
            {
                Guid = guid, 
                User = user
            };
            _context.Tokens.Add(token);
            return token;
        }

        public Token CreateUser(string userName, string fullName, string password)
        {
            var user = new User
            {
                FullName = fullName, 
                Username = userName, 
                Hash = GetHash(password)
            };
            _context.Users.Add(user);
            return CreateToken(user);
        }

        private string GetHash(string password)
        {
            return password;
        }

        private bool HashMatchesPassword(string password, string hash)
        {
            return hash == password;
        }
    }
}
