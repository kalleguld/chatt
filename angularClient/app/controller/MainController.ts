module dk.kalleguld.AngularChatt {
    export class MainController {

        private _scope:any;
        private _userService: IUserService;
        private _tokenService: ITokenService;
        private _messageService: IMessageService;

        
        constructor(scope:any, us: IUserService, ts:ITokenService, ms:IMessageService) {
            this._scope = scope;
            this._userService = us;
            this._tokenService = ts;
            this._messageService = ms;
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