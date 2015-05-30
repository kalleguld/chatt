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
            if (guidStr == null) new JsonMissingParameter("token").Throw();
            Guid guid;
            if (!Guid.TryParse(guidStr, out guid)) new JsonWrongParameterType("token", "guid").Throw();

            var token = connection.UserService.GetToken(guid);
            if (token == null) new JsonError(JsonErrorCode.InvalidToken).Throw();
            return token;
        }

        protected IUser GetUser(Connection connection, IToken token, string username)
        {
            if (username == null) new JsonMissingParameter("username").Throw();
            return connection.UserService.GetUser(token, username);
        }

        protected IUser GetFriend(Connection connection, IToken token, string username)
        {
            var friend = GetUser(connection, token, username);
            var success = (friend != null && connection.FriendService.HasAccessToUserDetails(token, friend));
            
            if (!success)
            {
                new JsonError(JsonErrorCode.UserNotFriendly).Throw();
            }
            return friend;
        }

        protected void CheckNull(object o, string name)
        {
            if (o == null) new JsonMissingParameter(name).Throw();
        }

    }

    
}
