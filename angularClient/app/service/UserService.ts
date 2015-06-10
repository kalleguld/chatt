module dk.kalleguld.AngularChatt {

    export class UserService implements IUserService, ITokenChangeListener, IMessageCreatedListener {

        private _friends: Map<string, User> = new Map<string, User>();
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
            this.updateFriends();
            tokenService.addTokenChangeListener(this);
            mls.addListener(this);
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
            var a = this._http.get<IRUserList>(url).success(this.friendListCallback);
        }

        friendListCallback: angular.IHttpPromiseCallback<IRUserList> = (data: IRUserList) => {
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

        messageCreated(messageId: number, partner: string): void {
            var user = this._friends[partner];
            if (user) this._messageService.getMessages(user);
        }
    }

    export interface IRUserListUser {
        username: string;
        fullName: string;
    }
    export interface IRUserList {
        users: Array<IRUserListUser>;
    }
}