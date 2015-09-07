module dk.kalleguld.AngularChatt {
    export class NewUserController {
        
        username = "";
        password1 = "";
        password2 = "";
        fullName = "";

        private _userService: IUserService;
        private _tokenService: ITokenService;
        private _locationService: ng.ILocationService;

        private _takenUsername = "";

        constructor(
                userService: IUserService,
                tokenService: ITokenService,
                locationService: ng.ILocationService) {

            this._userService = userService;
            this._tokenService = tokenService;
            this._locationService = locationService;
        }

        get showPasswordMismatchError(): boolean {
            return this.password1 !== this.password2 &&
                this.password1 !== "" &&
                this.password2 !== "";
        }

        get usernameTaken(): boolean {
            return this._takenUsername !== "" && this._takenUsername === this.username;
        }

        create() {
            //TODO Better error handling
            if (this.password1 !== this.password2) return;
            if (this.username === "") return;

            this._userService.createUser(this.username, this.password1, this.fullName, (u) => {
                this._tokenService.setCredentials(this.username, this.password1, (success) => {
                    if (success) {
                        this._locationService.path("/chat");
                    }
                });
            });
        }
    }
}