module dk.kalleguld.AngularChatt {
    export class LoginController {
        
        scope;
        tokenService: ITokenService;

        username: string;
        password: string;

        constructor(scope, tokenService) {
            this.scope = scope;
            this.tokenService = tokenService;
        }

        login():void {
            this.tokenService.setCredentials(this.username, this.password);
        }

        isLoggedIn(): boolean {
            return this.tokenService.isLoggedIn();
        }
    }
}