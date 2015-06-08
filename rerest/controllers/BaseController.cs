using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using modelInterface;
using rerest.jsonBase;
using rerest.viewmodel.exceptions;
using serviceInterface;

namespace rerest.controllers
{
    public abstract class BaseController
    {
        private readonly ConnectionFactory _connectionFactory;

        protected BaseController() : this(new ConnectionFactory()){}

        protected BaseController(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public JsonResponse GetOptions()
        {
            return new JsonResponse();
        }

        public JsonResponse GetNamedOptions(string input)
        {
            return new JsonResponse();
        }


        protected Connection GetConnection()
        {
            return _connectionFactory.GetConnection();
        }



        protected IToken GetToken(Connection connection, string guidStr)
        {
            if (guidStr == null) 
                throw new JsonMissingParameter("token").GetException();
            Guid guid;
            if (!Guid.TryParse(guidStr, out guid)) 
                throw new JsonWrongParameterType("token", "guid").GetException();

            var token = connection.UserService.GetToken(guid);
            if (token == null) throw new JsonError(JsonResponseCode.InvalidToken).GetException();
            return token;
        }

        protected IUser GetUser(Connection connection, IToken token, string username)
        {
            if (username == null) throw new JsonMissingParameter("username").GetException();
            return connection.UserService.GetUser(token, username);
        }

        protected IUser GetFriend(Connection connection, IToken token, string username)
        {
            CheckNull(username, "username");
            var friend = GetUser(connection, token, username);
            var success = (friend != null && connection.FriendService.HasAccessToUserDetails(token, friend));
            
            if (!success)
                throw new JsonError(JsonResponseCode.UserNotFriendly).GetException();

            return friend;
        }

        protected void CheckNull(object o, string name)
        {
            if (o == null) throw new JsonMissingParameter(name).GetException();
        }

    }

    
}
