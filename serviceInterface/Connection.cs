using System;
using backend;
using serviceInterface.service;

namespace serviceInterface
{
    public class Connection : IDisposable
    {
        private FriendService _friendService;
        private UserService _userService;
        private MessageService _messageService;

        private Context Context { get; }

        public UserService UserService => 
            _userService ?? (_userService = new UserService(Context));

        public FriendService FriendService => 
            _friendService ?? (_friendService = new FriendService(UserService));


        public MessageService MessageService => 
            _messageService ?? (_messageService = new MessageService(Context, UserService));


        internal Connection()
        {
            Context = new Context();
        }

        public int SaveChanges()
        {
            var result = Context.SaveChanges();
            return result;
        }


        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            Context?.Dispose();
        }
        #endregion



    }
}
