module dk.kalleguld.AngularChatt {
    export class LoginController {
        
        private _scope;
        private _location;
        private _tokenService: ITokenService;

        username: string;
        password: string;
        loginFailed = false;

        get isLoggedIn(): boolean {
            return this._tokenService.loggedIn;
        }

        get token(): string {
            return this._tokenService.token;
        }

        constructor(scope, location, tokenService: ITokenService) {
            this._scope = scope;
            this._location = location;
            this._tokenService = tokenService;
        }

        login():void {
            this.loginFailed = false;
            this._tokenService.setCredentials(this.username, this.password,(success) => {
                if (success) {
                    this._location.path("/");
                } else {
                    this.loginFailed = true;
                }
            });
        }
        
    }
}