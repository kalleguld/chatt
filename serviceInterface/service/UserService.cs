using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using backend.model;
using modelInterface;
using modelInterface.exceptions;

namespace serviceInterface.service
{
    public class UserService : BaseService
    {

        public static readonly Regex NameRegex = new Regex("^[a-zA-Z0-9-_]+$");

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
            if (!HashMatchesPassword(password, user.Hash)) return null;
            return CreateToken(user);
        }

        public void DeleteToken(IToken token)
        {
            Connection.Context.Tokens.Remove(GetToken(token));
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
            if (!IsValidUsername(username))
                throw new InvalidUsername(username, NameRegex);
            if (Connection.Context.Users.Any(u => u.Username == username))
                throw new UsernameExists(username);

            var user = new User
            {
                FullName = fullName,
                Username = username,
                Hash = CreateHash(password)
            };
            Connection.Context.Users.Add(user);
            return user;
        }

        public IEnumerable<IUser> GetUsers(IToken token, string filter = null)
        {
            IEnumerable<User> result = Connection.Context.Users;
            if (filter != null)
            {
                result = result.Where(u => u.Username.Contains(filter));
            }
            return result;
        }

        private static string CreateHash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(10));
        }

        private static bool HashMatchesPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        internal User GetUser(IUser iUser)
        {
            return iUser as User ?? Connection.Context.Users.First(u => u.Username == iUser.Username);
        }

        internal Token GetToken(IToken iToken)
        {
            return iToken as Token ?? Connection.Context.Tokens.First(t => t.Guid == iToken.Guid);
        }

        public static bool IsValidUsername(string name)
        {
            return name != null && NameRegex.IsMatch(name);
        }
    }
}
