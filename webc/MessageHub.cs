using System;
using System.Linq;
using System.Threading.Tasks;
using modelInterface;
using Microsoft.AspNet.SignalR;
using serviceInterface;
using WcfWsComms;

namespace webc
{
    public class MessageHub : Hub
    {
        private static Task _dbChecker;
        private static bool requestClose = false;
        private static readonly ConnectionFactory ConnectionFactory = new ConnectionFactory();

        public void Login(string tokenStr)
        {
            using (var dbConn = ConnectionFactory.GetConnection())
            {
                var token = GetToken(dbConn, tokenStr);
                if (token == null) return;

                Groups.Add(Context.ConnectionId, token.User.Username);
                OpenServiceConnection();
            }
        }


        private static IToken GetToken(Connection connection, string guidStr)
        {
            if (guidStr == null) return null;
            Guid guid;
            if (!Guid.TryParse(guidStr, out guid)) return null;
            return connection.UserService.GetToken(guid);
        }



        private static void OpenServiceConnection()
        {
            if (_dbChecker != null && !_dbChecker.IsCompleted) return;

            _dbChecker = new Task(() =>
            {
                var lastMessageId = 0;

                using (var conn = ConnectionFactory.GetConnection())
                {
                    var newMessages = conn.MessageService.GetMessages(lastMessageId);
                    var lastMessage = newMessages.OrderByDescending(m => m.Id).FirstOrDefault();
                    if (lastMessage != null) lastMessageId = lastMessage.Id;
                }

                while (!requestClose)
                {
                    using (var conn = ConnectionFactory.GetConnection())
                    {
                        var newMessages = conn.MessageService.GetMessages(lastMessageId);
                        foreach (var newMessage in newMessages)
                        {
                            NewMessage(new MessageInfo
                            {
                                MessageId = newMessage.Id, 
                                Sender = newMessage.Sender.Username,
                                Receiver = newMessage.Receiver.Username
                            });
                            lastMessageId = newMessage.Id;
                        }
                    }
                    Task.Delay(1000);
                }
            });
            _dbChecker.Start();
        }

        private static void NewMessage(MessageInfo message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            hubContext.Clients.Group(message.Receiver).NewMessageCreated(message.MessageId, message.Sender, message.Receiver);
            hubContext.Clients.Group(message.Sender).NewMessageCreated(message.MessageId, message.Sender, message.Receiver);
        }
    }
}