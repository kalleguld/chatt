using System;
using System.Collections.Generic;
using System.Linq;
using backend.model;
using modelInterface;
using modelInterface.exceptions;

namespace serviceInterface.service
{
    public class UserService : BaseService
    {
        internal UserService(Connection connection) : base(connection) { }

        internal User GetUser(string username)
        {
            return Connection.Context.Users.FirstOrDefault(u => u.Username == username);
        }

        public IUser GetUser(IToken token, string username)
        {
            var user = GetUser(username);
            if (Connection.FriendService.HasAccessToUserDetails(token, user)) return user;
            return null;
        }

        public IToken GetToken(Guid guid)
        {
            return Connection.Context.Tokens.FirstOrDefault(t => t.Guid == guid);
        }

        public IToken CreateToken(string username, string password)
        {
            var user = GetUser(username);
            if (user == null) return null;
            if (!HashMatchesPassword(password,user.Hash)) return null;
            return CreateToken(user);
        }

        private IToken CreateToken(IUser user)
        {
            var guid = Guid.NewGuid();
            while (Connection.Context.Tokens.Any(t => t.Guid == guid))
            {
                guid = Guid.NewGuid();
            }
            var token = new Token
            {
                Guid = guid, 
                User = GetUser(user)
            };
            Connection.Context.Tokens.Add(token);
            return token;
        }

        public IUser CreateUser(string username, string fullName, string password)
        {
            if (Connection.Context.Users.Any(u => u.Username == username))
            {
                throw new UsernameExists(username);
            }
            var user = new User
            {
                FullName = fullName, 
                Username = username, 
                Hash = GetHash(password)
            };
            Connection.Context.Users.Add(user);
            return user;
        }

        public IEnumerable<IUser> GetUsers()
        {
            return Connection.Context.Users.Cast<IUser>();
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
            return iUser as User ?? Connection.Context.Users.First(u => u.Username == iUser.Username);
        }

    }
}
