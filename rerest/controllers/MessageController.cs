using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using modelInterface;
using modelInterface.exceptions;
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
                          "&getReceived={getReceivedStr}" +
                          "&maxResults={maxResultsStr}")]
        public MessageList GetMessages(string guidStr, 
            string sender, string afterIdStr, string afterTimestampStr, 
            string getSentStr, string getReceivedStr, string maxResultsStr)
        {
            int? afterId = IntUtils.ParseN(afterIdStr);
            int? maxResults = IntUtils.ParseN(maxResultsStr);

            using (var connection = GetConnection())
            {
                var token = GetToken(connection, guidStr);
                var messages = connection.MessageService.GetMessages(
                    token, 
                    BoolUtils.ParseN(getSentStr) ?? true,
                    BoolUtils.ParseN(getReceivedStr) ?? true,
                    sender,
                    afterId,
                    DateUtils.FromMilisN(afterTimestampStr),
                    maxResults);
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
                throw new JsonWrongParameterType("messageId", "int").GetException();
            }

            using (var conn = GetConnection())
            {
                var token = GetToken(conn, guidStr);
                var message = conn.MessageService.GetMessage(token, messageId);
                if (message == null)
                {
                    throw new JsonError(JsonResponseCode.AccessDenied).GetException();
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
                IMessage message;
                try
                {
                    message = conn.MessageService.CreateMessage(token, receiver, contents);
                }
                catch (MessageContentEmpty)
                {
                    throw new JsonMissingParameter("contents").GetException();
                }
                catch (InsufficientRights)
                {
                    throw new JsonError(JsonResponseCode.UserNotFriendly).GetException();
                }
                
                conn.SaveChanges();
                return new MessageInfo(message, true);
            }
            
        }
    }
}
