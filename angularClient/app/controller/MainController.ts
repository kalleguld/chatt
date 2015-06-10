module dk.kalleguld.AngularChatt {
    export class MainController {

        private _scope: any;
        private _rootScope: IRootScope;
        private _userService: IUserService;
        private _tokenService: ITokenService;
        private _messageService: IMessageService;

        
        constructor(scope: ng.IScope, rootScope: IRootScope,
                us: IUserService, ts: ITokenService, ms: IMessageService) {
            this._scope = scope;
            this._rootScope = rootScope;
            this._userService = us;
            this._tokenService = ts;
            this._messageService = ms;
            rootScope.title = "Main";
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

        updateHistory(user: User): void {
            this._messageService.getMessages(user);
        }

    }
}