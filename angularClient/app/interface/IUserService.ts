module dk.kalleguld.AngularChatt {

    export interface IUserService {

        
        friends: { [key: string]: User }

        friendRequests: Object;

        updateFriends(): void;

        updateFriendRequests(): void;

        markMessagesRead(user: User): void;

        addFriend(username: string): void;

        deleteFriendRequest(user: User): void;

        createUser(
            username: string,
            password: string,
            fullName: string,
            callback: (user: IRUserListUser) => void) : void;
    }
}