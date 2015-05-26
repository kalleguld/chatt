using System;
using System.Linq;
using modelInterface;
using rerest.models.output.exceptions;
using serviceInterface.service;

namespace rerest.controllers
{
    public class BaseController
    {
        protected readonly ConnectionFactory ConnectionFactory;
        public BaseController() { ConnectionFactory = new ConnectionFactory(); }

        protected Connection GetConnection()
        {
            return ConnectionFactory.GetConnection();
        }

        protected IToken GetToken(Connection connection, string guidStr)
        {
            if (guidStr == null) throw new JsonBadRequest("you need a token parameter");
            Guid guid;
            if (!Guid.TryParse(guidStr, out guid)) throw new JsonBadRequest("Token needs to be a guid");

            var token = connection.UserService.GetToken(guid);
            if (token == null) throw new JsonInvalidToken();
            return token;
        }

        protected IUser GetUser(Connection connection, string username)
        {
            if (username == null) throw new JsonBadRequest("you need a username parameter");
            var user = connection.UserService.GetUser(username);
            if (user == null) throw new JsonNotFound("The specified user was not found. Are you sure you spelled the username correctly?");
            return user;
        }

        protected IUser GetFriend(Connection connection, IToken token, string username)
        {
            var friend= GetUser(connection, username);
            if (!token.User.Friends.Contains(friend))
            {
                throw new JsonException(403, 6, 
                    "That user is not a friend of yours", 
                    "That user is not a friend of yours");
            }
            return friend;
        }

        protected void CheckNull(object o, string name)
        {
            if (o == null) throw new JsonBadRequest(name + " parameter is needed.");
        }
    }

    
}
