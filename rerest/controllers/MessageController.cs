using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel.exceptions;
using rerest.viewmodel;

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

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "{messageIdStr}/?token={guidStr}")]
        public MessageInfo GetMessage(string guidStr, string messageIdStr)
        {
            int messageId;
            if (!int.TryParse(messageIdStr, out messageId))
            {
                new JsonWrongParameterType("messageId", "int").Throw();
            }

            using (var conn = GetConnection())
            {
                var token = GetToken(conn, guidStr);
                var message = conn.MessageService.GetMessage(token, messageId);
                if (message == null)
                {
                    new JsonError(JsonResponseCode.AccessDenied).Throw();
                }
                return new MessageInfo(message);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?token={guidStr}&receiver={receiver}&content={content}")]
        public MessageInfo CreateMessage(string guidStr, string receiver, string content)
        {
            using (var conn = GetConnection())
            {
                var token = GetToken(conn, guidStr);
                CheckNull(receiver, "receiver");
                CheckNull(content, "content");
                var message = conn.MessageService.CreateMessage(token, receiver, content);
                if (message == null)
                {
                    new JsonError(JsonResponseCode.UserNotFriendly).Throw();
                }
                conn.SaveChanges();
                return new MessageInfo(message);
            }
            
        }
    }
}
