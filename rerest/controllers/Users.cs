using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using backend.exceptions;
using backend.Service;
using rerest.models.@base;
using rerest.models.input;
using rerest.models.output;
using rerest.models.output.exceptions;

namespace rerest.controllers
{
    [ServiceContract]
    public class Users : BaseController
    {

        [OperationContract]
        [WebInvoke(
            Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "{username}")]
        public UserInfo GetUser(string username)
        {
            using (var context = ContextFactory.GetContext())
            {
                var userService = new UserService(context);
                var user = userService.GetUser(username);
                
                if (user == null) throw new JsonNotFound();

                return new UserInfo(user);
            }
        }

        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "")]
        public TokenInfo CreateUser(NewUserInfo userInfo)
        {
            try
            {
                using (var context = ContextFactory.GetContext())
                {
                    var userService = new UserService(context);
                    var token = userService.CreateUser(userInfo.Username, userInfo.FullName, userInfo.Password);
                    context.SaveChanges();
                    return new TokenInfo(token);
                }
            }
            catch (UsernameExists)
            {
                throw new JsonUsernameExists(userInfo.Username);
            }
        }
    }
}
