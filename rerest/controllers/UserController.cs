using System.ServiceModel;
using System.ServiceModel.Web;
using modelInterface;
using modelInterface.exceptions;
using rerest.jsonBase;
using rerest.viewmodel;
using rerest.viewmodel.exceptions;

namespace rerest.controllers
{
    [ServiceContract]
    public interface IUserController : IOptionController
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?username={username}&password={password}&fullname={fullName}")]
        UserInfo CreateUser(string username, string password, string fullName);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?filter={filter}")]
        UserList GetUsers(string filter);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{username}/")]
        UserInfo GetFriend(string username);
    }

    public class UserController : BaseController, IUserController
    {

        public UserInfo CreateUser(string username, string password, string fullName)
        {
            CheckNull(username, "username");
            CheckNull(password, "password");
            CheckNull(fullName, "fullname");

            using (var connection = GetConnection())
            {
                IUser user;
                try
                {
                    user = connection.UserService.CreateUser(username, fullName, password);
                }
                catch (UsernameExists)
                {
                    throw new JsonError(JsonResponseCode.UsernameExists).GetException();
                }
                catch (InvalidUsername)
                {
                    throw new JsonError(JsonResponseCode.InvalidUsername).GetException();
                }
                connection.SaveChanges();
                return new UserInfo(user);
            }
        }

        public UserInfo GetSelf()
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection);
                return new UserInfo(token.User);
            }
        }

        public UserList GetUsers(string filter)
        {
            using (var conn = GetConnection())
            {
                var token = GetToken(conn);
                var users = conn.UserService.GetUsers(token, filter);
                return new UserList(users);
            }
        }

        public UserInfo GetFriend(string username)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection);
                var friend = GetFriend(connection, token, username);
                return new UserInfo(friend);
            }
        }
    }
}
