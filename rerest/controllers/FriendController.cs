using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.models.output;

namespace rerest.controllers
{
    [ServiceContract]
    class FriendController : BaseController
    {

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}")]
        public UserList GetFriends(string guidStr)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var friends = connection.FriendService.GetFriends(token.User);
                return new UserList(friends);
            }
        }


        [OperationContract]
        [WebInvoke(Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}&username={username}")]
        public JsonResponse AddFriend(string guidStr, string username)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var friend = GetUser(connection, username);
                connection.FriendService.AddFriend(token, friend);
                connection.SaveChanges();
                return new JsonResponse();
            }
        }

        [OperationContract]
        [WebInvoke(Method = "DELETE", 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?token={guidStr}&username={username}")]
        public JsonResponse RemoveFriend(string guidStr, string username)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var friend = GetUser(connection, username);
                connection.FriendService.RemoveFriend(token, friend);
                connection.SaveChanges();
                return new JsonResponse();
            }
        }

    }
}
