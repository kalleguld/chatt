using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel;

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
                return new UserList(token.User.Friends);
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
