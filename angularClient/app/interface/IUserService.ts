﻿module dk.kalleguld.AngularChatt {

    export interface IUserService {
        
        friends: Map<string,User>;

        updateFriends(): void;

    }
}