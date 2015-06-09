module dk.kalleguld.AngularChatt {
    
    class UserService implements IUserService, ITokenChangeListener {
        
        private _friends: Map<string, User> = new Map<string, User>();
        private _tokenService: ITokenService;
        private _rerestService: IRerestService;
        private _messageService:IMessageService;
        private _http: ng.IHttpService;

        constructor(http: ng.IHttpService, tokenService: ITokenService, rerestService: IRerestService,
            ms:IMessageService) {
            this._http = http;
            this._tokenService = tokenService;
            this._rerestService = rerestService;
            this._messageService = ms;
            this.updateFriends();
            tokenService.addTokenChangeListener(this);
        }

        get friends(): Map<string, User> {
            return this._friends;
        }

        updateFriends(): void {
            if (!this._tokenService.loggedIn) {
                this._friends = new Map<string, User>();
                return;
            }
            var url = this._rerestService.getUrl("friends/", { token: this._tokenService.token });
            var a = this._http.get<RUserList>(url).success(this.friendListCallback);
        }

        friendListCallback: angular.IHttpPromiseCallback<RUserList> = (data: RUserList) => {
            for (var i = 0; i < data.users.length; i++) {
                var rUser = data.users[i];
                var user = new User();
                user.username = rUser.username;
                user.fullName = rUser.fullName;
                this._friends[user.username] = user;
                this._messageService.getMessages(user);
            }
        }

        tokenChanged(token: string): void {
            this.updateFriends();
        }
    }

    class RUserListUser {
        username: string;
        fullName: string;
    }
    class RUserList {
        users:Array<RUserListUser>;
    }
}