module dk.kalleguld.AngularChatt {

    export class User {
        username: string;
        fullName: string;
        messages: Array<Message>;
        outMessage: string;
        unreadMessages: number = 0;

        constructor() {
            this.messages = new Array<Message>();
        }


    }
}