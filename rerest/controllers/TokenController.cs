using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel.exceptions;
using rerest.viewmodel;

namespace rerest.controllers
{
    [ServiceContract]
    class TokenController : BaseController
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?username={username}&password={password}")]
        public TokenInfo CreateToken(string username, string password)
        {
            CheckNull(username, "username");
            CheckNull(password, "password");
            using (var connection = GetConnection())
            {
                var token = connection.UserService.CreateToken(username, password);
                if (token == null) throw new JsonError(JsonResponseCode.LoginError).GetException();
                connection.SaveChanges();
                return new TokenInfo(token);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{guidStr}/")]
        public TokenInfo GetToken(string guidStr)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                return new TokenInfo(token);
            }
        }

    }
}
