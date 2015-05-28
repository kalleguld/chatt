using System.ServiceModel;
using System.ServiceModel.Web;
using modelInterface.exceptions;
using rerest.models.output;
using rerest.models.output.exceptions;

namespace rerest.controllers
{
    [ServiceContract]
    public class UserController : BaseController
    {

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?username={username}&password={password}&fullname={fullName}")]
        public UserInfo CreateUser(string username, string password, string fullName)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var user = connection.UserService.CreateUser(username, fullName, password);
                    connection.SaveChanges();
                    return new UserInfo(user);
                }
            }
            catch (UsernameExists)
            {
                throw new JsonUsernameExists(username);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}")]
        public UserInfo GetSelf(string guidStr)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                return new UserInfo(token.User);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{username}/?token={guidStr}")]
        public UserInfo GetFriend(string guidStr, string username)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var friend = GetFriend(connection, token, username);
                return new UserInfo(friend);
            }
        }
    }
}
