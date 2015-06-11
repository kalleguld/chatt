module dk.kalleguld.AngularChatt {
    export class MainController {

        private _scope: ng.IScope;
        private _rootScope: IRootScope;
        private _loc: ng.ILocationService;
        private _routeParams;
        private _userService: IUserService;
        private _tokenService: ITokenService;
        private _messageService: IMessageService;
        private _selectedUser: User;

        constructor(scope: ng.IScope, rootScope: IRootScope, location: ng.ILocationService, routeParams,
            us: IUserService, ts: ITokenService, ms: IMessageService) {
            this._scope = scope;
            this._rootScope = rootScope;
            this._loc = location;
            this._routeParams = routeParams;
            this._userService = us;
            this._tokenService = ts;
            this._messageService = ms;
            if (routeParams.friend) {
                if (this._userService.friends[routeParams.friend]) {
                    this._selectedUser = this._userService.friends[routeParams.friend];
                } else {
                    this._loc.path("/chat");
                }
            } else {
                //no friend selected
                rootScope.title = "Main";
            }
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

    }
}