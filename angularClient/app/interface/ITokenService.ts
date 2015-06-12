module dk.kalleguld.AngularChatt {
    export interface ITokenService {

        loggedIn: boolean;
        token: string;
        username: string;

        setCredentials(username: string, password: string, callback:ILoginCallback): void;

        clearCredentials(): void;
        
        addTokenChangeListener(tcl: ITokenChangeListener):void;
    }

    export interface ITokenChangeListener {
        tokenChanged(token:string):void;
    }
    export interface ILoginCallback { (success: boolean): void; };

}