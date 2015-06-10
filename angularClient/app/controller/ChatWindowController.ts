module dk.kalleguld.AngularChatt {
    export class ChatWindowController {
        
        private _scope: any;

        constructor(scope: any) {
            this._scope = scope;
        }

        get messages(): Array<Message> {
            return this._scope.user.messages;
        }
    }
}