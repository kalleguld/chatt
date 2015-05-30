using System;
using modelInterface;
using rerest.models.output.exceptions;
using serviceInterface;

namespace rerest.controllers
{
    public class BaseController
    {
        private readonly ConnectionFactory _connectionFactory;

        public BaseController() : this(new ConnectionFactory()){}
        public BaseController(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected Connection GetConnection()
        {
            return _connectionFactory.GetConnection();
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

        protected IUser GetUser(Connection connection, IToken token, string username)
        {
            if (username == null) throw new JsonBadRequest("you need a username parameter");
            return connection.UserService.GetUser(token, username);
        }

        protected IUser GetFriend(Connection connection, IToken token, string username)
        {
            var friend = GetUser(connection, token, username);
            var success = (friend != null && connection.FriendService.HasAccessToUserDetails(token, friend));
            
            if (!success)
            {
                //throw the same exception even if the user doesn't exist
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
