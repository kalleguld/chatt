using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using backend.Service;
using rerest.models.input;
using rerest.models.output;
using rerest.models.output.exceptions;

namespace rerest.controllers
{
    [ServiceContract]
    class Friends : BaseController
    {

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}")]
        public UserList GetFriends(string guidStr)
        {
            using (var context = ContextFactory.GetContext())
            {
                var token = GetToken(context, guidStr);
                var friendService = new FriendService(context);
                var friends = friendService.GetFriends(token.User);
                return new UserList(friends);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{username}/?token={guidStr}")]
        public UserInfo GetFriend(string guidStr, string username)
        {
            using (var context = ContextFactory.GetContext())
            {
                var token = GetToken(context, guidStr);
                var friend = GetFriend(context, token, username);
                return new UserInfo(friend);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}&username={username}")]
        public JsonResponse AddFriend(string guidStr, string username)
        {
            using (var context = ContextFactory.GetContext())
            {
                var token = GetToken(context, guidStr);
                var friend = GetUser(context, username);
                var friendService = new FriendService(context);
                friendService.AddFriend(token, friend);
                context.SaveChanges();
                return new JsonResponse();
            }
        }

        [OperationContract]
        [WebInvoke(Method = "DELETE", 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?token={guidStr}&username={username}")]
        public JsonResponse RemoveFriend(string guidStr, string username)
        {
            using (var context = ContextFactory.GetContext())
            {
                var token = GetToken(context, guidStr);
                var friend = GetUser(context, username);
                var friendService = new FriendService(context);
                friendService.RemoveFriend(token, friend);
                context.SaveChanges();
                return new JsonResponse();
            }
        }

    }
}
