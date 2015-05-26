using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.models.output;

namespace rerest.controllers
{
    [ServiceContract]
    public class MessageController : BaseController
    {

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}")]
        public MessageList GetMessages(string guidStr)
        {
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var messages = connection.MessageService.GetMessages(token.User);
                var list = new MessageList(messages);
                return list;
            }
        }
    }
}
