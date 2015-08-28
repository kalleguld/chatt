using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel;

namespace rerest.controllers
{
    [ServiceContract]
    public interface IFriendController : IOptionController
    {
        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "")]
        UserList GetFriends();

        [OperationContract]
        [WebInvoke(Method = "DELETE", 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?username={username}")]
        JsonResponse RemoveFriend(string username);

    }

    public class FriendController : BaseController, IFriendController
    {

        public UserList GetFriends()
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection);
                return new UserList(token.User.Friends);
            }
        }

        public JsonResponse RemoveFriend(string username)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection);
                var friend = GetUser(connection, token, username);
                if (friend == null) return new JsonResponse();
                connection.FriendService.RemoveFriend(token, friend);

                connection.SaveChanges();
                return new JsonResponse();
            }
        }
    }
}
