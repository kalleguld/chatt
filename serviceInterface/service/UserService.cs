using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using backend;
using backend.model;
using modelInterface;
using modelInterface.exceptions;

namespace serviceInterface.service
{
    public class UserService
    {
        private readonly Context _context;


        internal UserService(Context context)
        {
            _context = context;
        }

        public IUser GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public IToken GetToken(Guid guid)
        {
            return _context.Tokens.FirstOrDefault(t => t.Guid == guid);
        }

        public IToken CreateToken(string username, string password)
        {
            var user = GetUser(username) as User;
            if (user == null) return null;
            if (!HashMatchesPassword(password,user.Hash)) return null;
            return CreateToken(user);
        }

        private IToken CreateToken(IUser user)
        {
            var guid = Guid.NewGuid();
            while (_context.Tokens.Any(t => t.Guid == guid))
            {
                guid = Guid.NewGuid();
            }
            var token = new Token
            {
                Guid = guid, 
                User = (User)user
            };
            _context.Tokens.Add(token);
            return token;
        }

        public IUser CreateUser(string username, string fullName, string password)
        {
            if (_context.Users.Any(u => u.Username == username)) throw new UsernameExists(username);
            var user = new User
            {
                FullName = fullName, 
                Username = username, 
                Hash = GetHash(password)
            };
            _context.Users.Add(user);
            return user;
        }

        public IEnumerable<IUser> GetUsers()
        {
            return _context.Users.Cast<IUser>();
        } 

        private static string GetHash(string password)
        {
            return password;
        }

        private static bool HashMatchesPassword(string password, string hash)
        {
            return hash == password;
        }

    }
}
