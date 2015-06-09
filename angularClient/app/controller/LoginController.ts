module dk.kalleguld.AngularChatt {
    export class LoginController {
        
        private _scope;
        private _tokenService: ITokenService;

        username: string;
        password: string;

        get isLoggedIn(): boolean {
            return this._tokenService.loggedIn;
        }

        get token(): string {
            return this._tokenService.token;
        }

        constructor(scope, tokenService: ITokenService) {
            this._scope = scope;
            this._tokenService = tokenService;
        }

        login():void {
            this._tokenService.setCredentials(this.username, this.password);
        }
        
    }
}