using System;
using backend;
using serviceInterface.service;

namespace serviceInterface
{
    public class Connection : IDisposable
    {
        internal Context Context { get { return _context; } }

        public FriendService FriendService { get
        {
            return _friendService ?? (_friendService = new FriendService(this));
        } }

        public UserService UserService{  get
        {
            return _userService ?? (_userService = new UserService(this));
        } }

        public MessageService MessageService { get
        {
            return _messageService ?? (_messageService = new MessageService(this));
        } }

        public MessageListenerService MessageListenerService { get
        {
            return _messageListenerService ?? (_messageListenerService = new MessageListenerService());
        } }



        private readonly Context _context;
        private FriendService _friendService;
        private UserService _userService;
        private MessageService _messageService;
        private MessageListenerService _messageListenerService;


        internal Connection()
        {
            _context = new Context();
        }

        public int SaveChanges()
        {
            var result = _context.SaveChanges();
            if (_messageListenerService != null)
            {
                MessageListenerService.DatabaseSaved();
            }
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
            if (_context != null) _context.Dispose();
        }
        #endregion



    }
}
