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
    public interface IFriendRequestController : IOptionController
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "")]
        UserList GetFriendRequests();

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{username}/")]
        FriendRequestResponse AddFriend(string username);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{username}/")]
        JsonResponse CancelFriendRequest(string username);
    }

    public class FriendRequestController : BaseController, IFriendRequestController
    {
        public UserList GetFriendRequests()
        {
            using (var conn = GetConnection())
            {
                var token = GetToken(conn);
                return new UserList(token.User.FriendRequests);
            }
        }

        public FriendRequestResponse AddFriend(string username)
        {
            CheckNull(username, "username");
            using (var connection = GetConnection())
            {
                var token = GetToken(connection);
                var response = connection.FriendService.RequestFriend(token, username);
                connection.SaveChanges();
                return new FriendRequestResponse(response);
            }
        }

        public JsonResponse CancelFriendRequest(string username)
        {
            CheckNull(username, "username");
            using (var conn = GetConnection())
            {
                var token = GetToken(conn);
                conn.FriendService.RemoveFriendRequest(token, username);
                conn.SaveChanges();
                return new JsonResponse();
            }
        }
    }
}
