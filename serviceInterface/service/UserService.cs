using System;
using System.Collections.Generic;
using System.Linq;
using backend.model;
using modelInterface;
using modelInterface.exceptions;

namespace serviceInterface.service
{
    public class UserService
    {
        private readonly Connection _conn;

        internal UserService(Connection conn)
        {
            _conn = conn;
        }

        internal User GetUser(string username)
        {
            return _conn.Context.Users.FirstOrDefault(u => u.Username == username);
        }

        public IUser GetUser(IToken token, string username)
        {
            var user = GetUser(username);
            if (_conn.FriendService.HasAccessToUserDetails(token, user)) return user;
            return null;
        }

        public IToken GetToken(Guid guid)
        {
            return _conn.Context.Tokens.FirstOrDefault(t => t.Guid == guid);
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
            while (_conn.Context.Tokens.Any(t => t.Guid == guid))
            {
                guid = Guid.NewGuid();
            }
            var token = new Token
            {
                Guid = guid, 
                User = GetUser(user)
            };
            _conn.Context.Tokens.Add(token);
            return token;
        }

        public IUser CreateUser(string username, string fullName, string password)
        {
            if (_conn.Context.Users.Any(u => u.Username == username))
            {
                throw new UsernameExists(username);
            }
            var user = new User
            {
                FullName = fullName, 
                Username = username, 
                Hash = GetHash(password)
            };
            _conn.Context.Users.Add(user);
            return user;
        }

        public IEnumerable<IUser> GetUsers()
        {
            return _conn.Context.Users.Cast<IUser>();
        } 

        private static string GetHash(string password)
        {
            return password;
        }

        private static bool HashMatchesPassword(string password, string hash)
        {
            return hash == password;
        }

        internal User GetUser(IUser iUser)
        {
            return iUser as User ?? _conn.Context.Users.First(u => u.Username == iUser.Username);
        }

    }
}
