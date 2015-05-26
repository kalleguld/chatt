using System;
using backend;

namespace serviceInterface.service
{
    public class Connection : IDisposable
    {
        public FriendService FriendService { get
        {
            return _friendService ?? (_friendService = new FriendService(_context));
        } }

        public UserService UserService{  get
        {
            return _userService ?? (_userService = new UserService(_context));
        } }

        public MessageService MessageService { get
        {
            return _messageService ?? (_messageService = new MessageService(_context));
        } }


        
        private readonly Context _context;
        private FriendService _friendService;
        private UserService _userService;
        private MessageService _messageService;


        internal Connection()
        {
            _context = new Context();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

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


    }
}
