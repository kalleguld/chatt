using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel.exceptions;
using rerest.viewmodel;

namespace rerest.controllers
{
    [ServiceContract]
    public interface ITokenController : IOptionController
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?username={username}&password={password}")]
        TokenInfo CreateToken(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "")]
        TokenInfo GetToken();

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "")]
        JsonResponse DeleteToken();
    }

    public class TokenController : BaseController, ITokenController
    {
        
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
        
        public TokenInfo GetToken()
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection);
                return new TokenInfo(token);
            }
        }

        public JsonResponse DeleteToken()
        {
            using (var conn = GetConnection())
            {
                var token = GetToken(conn);
                if (token == null) throw new JsonError(JsonResponseCode.LoginError).GetException();
                conn.UserService.DeleteToken(token);
                conn.SaveChanges();
                return new JsonResponse();
            }
        }
    }
}
