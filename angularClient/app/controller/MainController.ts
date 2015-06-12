module dk.kalleguld.AngularChatt {
    export class MainController implements IMessageCreatedListener {

        private _scope: ng.IScope;
        private _rootScope: IRootScope;
        private _loc: ng.ILocationService;
        private _routeParams;
        private _timeout:ng.ITimeoutService;
        private _messageListenerService:IMessageListenerService;
        private _userService: IUserService;
        private _tokenService: ITokenService;
        private _messageService: IMessageService;
        private _selectedUser: User;

        constructor(scope: ng.IScope, rootScope: IRootScope, location: ng.ILocationService, routeParams,
            timeout:ng.ITimeoutService,
            us: IUserService, ts: ITokenService, ms: IMessageService, mls: IMessageListenerService) {
            this._scope = scope;
            this._rootScope = rootScope;
            this._loc = location;
            this._routeParams = routeParams;
            this._timeout = timeout;
            this._messageListenerService = mls;
            this._userService = us;
            this._tokenService = ts;
            this._messageService = ms;

            scope.$on("$destroy",() => { this.destructor() });

            this.parseRoute();

            mls.addListener(this);

            if (this._selectedUser) {
                this.scrollDown();
            }
        }

        private destructor() {
            this._messageListenerService.removeListener(this);
        }

        private parseRoute() {
            if (this._routeParams.friend) {
                if (this._userService.friends[this._routeParams.friend]) {
                    this._selectedUser = this._userService.friends[this._routeParams.friend];
                    this._rootScope.title = this._selectedUser.username;
                } else {
                    this._loc.path("/chat");
                }
            } else {
                //no friend selected
                this._rootScope.title = "Main";
            }
        }
        private scrollDown():void {
            if (!this.selectedUser) return;
            this._timeout(() => {
                this._userService.markMessagesRead(this._selectedUser);
                this._scope.$broadcast("scrollDown");
                this._scope.$apply();
            }, 100, false);
        }

        get friends(): Map<string, User> {
            return this._userService.friends;
        }

        get loggedIn(): boolean {
            return this._tokenService.loggedIn;
        }

        get token(): string {
            return this._tokenService.token;
        }

        get username(): string {
            return this._tokenService.username;
        }

        get selectedUser(): User {
            return this._selectedUser;
        }

        selectUser(user: User): void {
            this._loc.path("/chat/" + user.username);
        }

        updateHistory(user: User): void {
            this._messageService.getMessages(user);
        }

        sendMessage(user: User): void {
            this._messageService.sendMessage(user);
        }

        prettyDate(d: Date): string {

            var now = new Date();
            var offsetMs = now.getDate() - d.getDate();
            var oneDayMs = 1000 * 60 * 60 * 24;

            if (offsetMs < oneDayMs) {
                return d.toLocaleTimeString();
            } else {
                return d.toLocaleDateString();
            }
        }

        logout():void {
            this._tokenService.clearCredentials();
            this._loc.path("/login");
        }

        messageCreated(messageId: number, partner: string):void {
            if (this._selectedUser && this._selectedUser.username === partner) {
                this.scrollDown();
            }
        }
    }
}