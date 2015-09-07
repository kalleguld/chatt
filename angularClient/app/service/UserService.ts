module dk.kalleguld.AngularChatt {

    export class UserService implements IUserService, ITokenChangeListener, IMessageCreatedListener {

        private _friends: { [key: string]: User } = {};
        private _friendRequests = {};

        private _tokenService: ITokenService;
        private _rerestService: IRerestService;
        private _messageService: IMessageService;
        private _messageListenerService: IMessageListenerService;
        private _http: ng.IHttpService;

        constructor(http: ng.IHttpService, tokenService: ITokenService, rerestService: IRerestService,
            ms: IMessageService, mls: IMessageListenerService) {
            this._http = http;
            this._tokenService = tokenService;
            this._rerestService = rerestService;
            this._messageService = ms;
            this._messageListenerService = mls;
            tokenService.addTokenChangeListener(this);
            this.tokenChanged(tokenService.token);
            mls.addListener(this);
        }

        get friends(): { [key: string]: User } {
            return this._friends;
        }

        get friendRequests(): Object {
            return this._friendRequests;
        }

        updateFriends(): void {
            this._friends = {};
            if (!this._tokenService.loggedIn) {
                return;
            }
            var url = this._rerestService.getUrl("friends/", {});
            this._http.get<IRUserList>(url).success((data: IRUserList) => {
                var newFriends = {};
                for (var i = 0; i < data.users.length; i++) {
                    var user = this.parseRUser(data.users[i]);
                    newFriends[user.username] = null;
                    if (!(user.username in this._friends)) {
                        this._friends[user.username] = user;
                        this._messageService.getMessages(user);
                    }
                }
                for (var username in this._friends) {
                    if (!(username in newFriends)) {
                        delete this._friends[username];
                    }
                }
            });
        }

        updateFriendRequests(): void {
            if (!this._tokenService.loggedIn) {
                this._friendRequests = {};
                return;
            }

            var url = this._rerestService.getUrl("friendrequests/", {});
            this._http.get<IRUserList>(url).success((data: IRUserList) => {
                var newRequests = {};
                for (var i = 0; i < data.users.length; i++) {
                    var user = this.parseRUser(data.users[i]);
                    newRequests[user.username] = null;
                    this._friendRequests[user.username] = user;
                }
                for (var username in this._friendRequests) {
                    if (!(username in newRequests)) {
                        delete this._friendRequests[username];
                    }
                }
                
            });
        }

        addFriend(username: string): void {
            var url = this._rerestService.getUrl(`friendrequests/${username}/`,{});
            this._http.post<IRFriendRequestResponse>(url, {})
                .success((response) => {
                    if (response.friendAdded) {
                        this.updateFriends();
                        this.updateFriendRequests();
                    }
                });
        }

        private parseRUser(rUser: IRUserListUser):User {
            var user = new User();
            user.username = rUser.username;
            user.fullName = rUser.fullName;
            return user;
        }

        tokenChanged(token: string): void {
            this.updateFriends();
            this.updateFriendRequests();
        }

        messageCreated(messageId: number, partner: string): void {
            var user = this._friends[partner];
            if (user) this._messageService.getMessages(user);
        }

        markMessagesRead(user: User): void {
            user.unreadMessages = 0;
        }

        deleteFriendRequest(user: User): void {
            var url = this._rerestService.getUrl("friendRequests/" + user.username + "/", {});
            this._http.delete(url).success(() => {
                this.updateFriendRequests();
            });
        }

        createUser(
                username: string,
                password: string,
                fullName: string,
                callback: (user: IRUserListUser) => void):void {

            var url = this._rerestService.getUrl("users/", {
                username: username,
                password: password,
                fullName:fullName
            });
            this._http.post(url, {}).success((u:IRUserListUser) => {
                callback(u);
            });
        }
    }

    export interface IRUserListUser {
        username: string;
        fullName: string;
    }
    export interface IRUserList {
        users: Array<IRUserListUser>;
    }

    export interface IRFriendRequestResponse {
        friendAdded: boolean;
    }
    
}