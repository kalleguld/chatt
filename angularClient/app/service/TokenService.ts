module dk.kalleguld.AngularChatt {

    export class TokenService implements ITokenService {
        
        static injName = "TokenService";

        http: ng.IHttpService;
        rerestService: IRerestService;
        token: string = null;
        username:string = null;

        constructor($http: ng.IHttpService, rerestService:IRerestService) {
            this.http = $http;
            this.rerestService = rerestService;
        }

        setCredentials(username: string, password: string): void {
            var url = this.rerestService.getUrl("tokens/", {
                username: username,
                password: password
            });
            this.http.post(url, {})
                .success((result: any) => {
                    this.token = result.token;
                    this.username = result.username;
                })
                .error(() => { alert("errrrror") });
        }

        clearCredentials(): void {
            this.token = null;
            this.username = null;
        }

        isLoggedIn(): boolean { return this.token == null; }
        getToken(): string { return this.token; }
        getUsername():string { return this.username; }
    }
}