module dk.kalleguld.AngularChatt {

    export interface IUserService {

        friends: Map<string, User>;

        friendRequests: Object;

        updateFriends(): void;

        updateFriendRequests(): void;

        markMessagesRead(user: User): void;

        addFriend(username: string): void;

        deleteFriendRequest(user: User): void;
    }
}