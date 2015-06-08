module dk.kalleguld.AngularChatt {
    export interface ITokenService {



        setCredentials(username: string, password: string): void;
        clearCredentials(): void;
        isLoggedIn(): boolean;
        getToken(): string;
        getUsername():string;
    }
}