using System;
using backend;
using backend.model;
using backend.Service;
using rerest.models.output.exceptions;

namespace rerest.controllers
{
    public class BaseController
    {
        public ContextFactory ContextFactory { get; set; }
        public BaseController() { ContextFactory = new ContextFactory(); }

        protected Token GetToken(Context context, string guidStr)
        {
            if (guidStr == null) throw new JsonBadRequest("you need a token parameter");
            Guid guid;
            if (!Guid.TryParse(guidStr, out guid)) throw new JsonBadRequest("Token needs to be a guid");

            var userService = new UserService(context);
            var token = userService.GetToken(guid);
            if (token == null) throw new JsonInvalidToken();
            return token;
        }

        protected User GetUser(Context context, string username)
        {
            if (username == null) throw new JsonBadRequest("you need a username parameter");
            var userService = new UserService(context);
            var user = userService.GetUser(username);
            if (user == null) throw new JsonNotFound("The specified user was not found. Are you sure you spelled the username correctly?");
            return user;
        }

        protected User GetFriend(Context context, Token token, string username)
        {
            var friend= GetUser(context, username);
            if (!token.User.Friends.Contains(friend))
            {
                throw new JsonException(403, 6, 
                    "That user is not a friend of yours", 
                    "That user is not a friend of yours");
            }
            return friend;
        }
    }

    
}
