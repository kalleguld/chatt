﻿module dk.kalleguld.AngularChatt {

    export class User {
        username: string;
        fullName: string;
        messages: Array<Message>;

        constructor() {
            this.messages = new Array<Message>();
        }

    }
}