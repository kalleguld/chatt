using System.ServiceModel;
using System.ServiceModel.Web;
using rerest.jsonBase;
using rerest.viewmodel;
using rerest.viewmodel.exceptions;
using utils;

namespace rerest.controllers
{
    [ServiceContract]
    public class MessageController : BaseController
    {

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            UriTemplate = "?token={guidStr}" +
                          "&sender={sender}" +
                          "&afterId={afterIdStr}" +
                          "&afterTimestamp={afterTimestampStr}" +
                          "&getSent={getSentStr}" +
                          "&getReceived={getReceivedStr}")]
        public MessageList GetMessages(string guidStr, 
            string sender, string afterIdStr, string afterTimestampStr, 
            string getSentStr, string getReceivedStr)
        {
            int? afterId = IntUtils.ParseN(afterIdStr);
            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var messages = connection.MessageService.GetMessages(
                    token, 
                    BoolUtils.ParseN(getSentStr) ?? true,
                    BoolUtils.ParseN(getReceivedStr) ?? true,
                    sender,
                    afterId,
                    DateUtils.FromMilisN(afterTimestampStr));
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
                    return null;
                }
                return new MessageInfo(message, message.Sender.Username == token.User.Username);
            }
        }

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "?token={guidStr}&receiver={receiver}&contents={contents}")]
        public MessageInfo CreateMessage(string guidStr, string receiver, string contents)
        {
            using (var conn = GetConnection())
            {
                var token = GetToken(conn, guidStr);
                CheckNull(receiver, "receiver");
                CheckNull(contents, "contents");
                var message = conn.MessageService.CreateMessage(token, receiver, contents);
                if (message == null)
                {
                    new JsonError(JsonResponseCode.UserNotFriendly).Throw();
                }
                conn.SaveChanges();
                return new MessageInfo(message, true);
            }
            
        }
    }
}
