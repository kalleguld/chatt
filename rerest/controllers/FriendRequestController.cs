using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using rerest.jsonBase;
using rerest.viewmodel;

namespace rerest.controllers
{
    [ServiceContract]
    class FriendRequestController : BaseController
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?token={guidStr}")]
        public UserList GetFriendRequests(string guidStr)
        {
            using (var conn = GetConnection())
            {
                var token = GetToken(conn, guidStr);
                return new UserList(token.User.FriendRequests);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{username}/?token={guidStr}")]
        public FriendRequestResponse AddFriend(string guidStr, string username)
        {
            CheckNull(username, "username");
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var response = connection.FriendService.RequestFriend(token, username);
                connection.SaveChanges();
                return new FriendRequestResponse(response);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{username}/?token={guidStr}")]
        public JsonResponse CancelFriendRequest(string guidStr, string username)
        {
            CheckNull(username, "username");
            using (var conn = GetConnection())
            {
                var token = GetToken(conn, guidStr);
                conn.FriendService.RemoveFriendRequest(token, username);
                conn.SaveChanges();
                return new JsonResponse();
            }
        }
    }
}
