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
        public FriendList GetFriends(string guidStr)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                return new FriendList(token.User);
            }
        }


        [OperationContract]
        [WebInvoke(Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}&username={username}")]
        public FriendRequestResponse AddFriend(string guidStr, string username)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var friend = GetUser(connection, token, username);
                if (friend == null)
                {
                    return new FriendRequestResponse(false);
                }
                var response = connection.FriendService.RequestFriend(token, friend);

                connection.SaveChanges();
                return new FriendRequestResponse(response);
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
                var friend = GetUser(connection, token, username);
                if (friend == null) return new JsonResponse();
                connection.FriendService.RemoveFriend(token, friend);

                connection.SaveChanges();
                return new JsonResponse();
            }
        }

    }
}
