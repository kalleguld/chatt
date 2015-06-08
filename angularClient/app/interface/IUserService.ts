module dk.kalleguld.AngularChatt {

    export interface IUserService {
        getUser(username: string): User;
        getFriends() : Array<User>;
    }
}