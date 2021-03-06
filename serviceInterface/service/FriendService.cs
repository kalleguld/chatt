﻿using System.Collections.Generic;
using System.Linq;
using modelInterface;

namespace serviceInterface.service
{
    public class FriendService
    {
        private readonly UserService _userService;

        internal FriendService(UserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<IUser> GetFriends(IUser user)
        {
            return user.Friends;
        } 

        public bool RequestFriend(IToken iToken, string friendName)
        {
            var user = _userService.GetUser(iToken.User);
            var friend = _userService.GetUser(friendName);
            if (friend == null) return false;
            if (user == friend) return true;
            if (user.FriendRequests.Contains(friend))
            {
                user.FriendRequests.Remove(friend);
                user.Friends.Add(friend);
                friend.Friends.Add(user);
                return true;
            }
            if (user.Friends.Contains(friend))
            {
                return true;
            }
            friend.FriendRequests.Add(user);
            return false;
        }

        public void RemoveFriend(IToken iToken, IUser iFriend)
        {
            var user = _userService.GetUser(iToken.User);
            var friend = _userService.GetUser(iFriend);
            user.Friends.Remove(friend);
            friend.Friends.Remove(user);
        }

        public void RemoveFriendRequest(IToken iToken, string friendName)
        {
            var user = _userService.GetUser(iToken.User);
            var friend = _userService.GetUser(friendName);
            user.FriendRequests.Remove(friend);
            friend.FriendRequests.Remove(user);
        }

        public bool HasAccessToUserDetails(IToken token, IUser friend)
        {
            if (token.User.Friends.Contains(friend)) return true;
            if (token.User.FriendRequests.Contains(friend)) return true;
            return false;
        }
    }
}
