using System.ServiceModel;
using System.ServiceModel.Web;
using backend.Service;
using rerest.models.input;
using rerest.models.output;
using rerest.models.output.exceptions;

namespace rerest.controllers
{
    [ServiceContract]
    public class Tokens : BaseController
    {
        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "")]
        public TokenInfo CreateToken(LoginInfo loginInfo)
        {
            using (var context = ContextFactory.GetContext())
            {
                var userService = new UserService(context);
                var token = userService.CreateToken(loginInfo.Username, loginInfo.Password);
                if (token == null) throw new JsonLoginError();
                context.SaveChanges();
                return new TokenInfo(token);
            }
        }
    }
}
