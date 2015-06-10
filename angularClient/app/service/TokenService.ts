module dk.kalleguld.AngularChatt {

    export class TokenService implements ITokenService {
        
        static injName = "TokenService";

        private _http: ng.IHttpService;
        private _rerestService: IRerestService;
        private _token: string = null;
        private _username: string = null;
        private _tokenChangeListeners: Array<ITokenChangeListener> = new Array();

        get username(): string {
            return this._username;
        }

        get token(): string {
            return this._token;
        }

        private set pToken(token: string) {
            var sendChange = (this._token !== token);
            this._token = token;
            if (sendChange) {
                for (var i = 0; i < this._tokenChangeListeners.length; i++) {
                    this._tokenChangeListeners[i].tokenChanged(token);
                }
            }
        }

        get loggedIn(): boolean {
            return this.token != null && this.token !== "";
        }

        constructor($http: ng.IHttpService, rerestService:IRerestService) {
            this._http = $http;
            this._rerestService = rerestService;
        }

        setCredentials(username: string, password: string, callback:ILoginCallback): void {
            this.pToken = null;
            this._username = null;

            var url = this._rerestService.getUrl("tokens/", {
                username: username,
                password: password
            });
            this._http.post(url, {})
                .success((result: IRTokenInfo) => {
                    this.pToken = result.token;
                    this._username = result.username;
                    if (callback) callback(true);
                })
                .error(() => {
                    if (callback) callback(false);
                });
        }

        clearCredentials(): void {

            this.pToken = null;
            this._username = null;
        }

        addTokenChangeListener(tcl: ITokenChangeListener): void {
            this._tokenChangeListeners.push(tcl);
        }
        
    }
    
    interface IRTokenInfo {
        token: string;
        username:string;
    }
}