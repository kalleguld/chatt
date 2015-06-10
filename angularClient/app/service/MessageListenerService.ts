module dk.kalleguld.AngularChatt {
    //import jquery
    declare var $;

    export class MessageListenerService implements ITokenChangeListener {

        private _tokenService: ITokenService;
        private _connection;
        private _messageListeners: Array<IMessageCreatedListener> = [];

        constructor(token: ITokenService) {
            this._tokenService = token;
            this._connection = $.connection;
            this._connection.messageHub.client.newMessageCreated = this.getMessageHandler();
            this._tokenService.addTokenChangeListener(this);
            this.tokenChanged(this._tokenService.token);
        }

        addListener(listener: IMessageCreatedListener) {
            this._messageListeners.push(listener);
        }

        removeListener(listener: IMessageCreatedListener) {
            for (var i = this._messageListeners.length - 1; i >= 0; i--) {
                var ml = this._messageListeners[i];
                if (ml === listener) {
                    this._messageListeners.splice(i, 1);
                }
            }
        }

        private login(token:string): void {
            this._connection.hub.start().done(() => {
                this._connection.messageHub.server.login(token);
            });
        }

        private logout(): void {
            this._connection.hub.stop();
        }

        private getMessageHandler() {
            var messageListeners = this._messageListeners;
            return (messageId: number, partner: string) => {
                for (var i in messageListeners) {
                    if (messageListeners.hasOwnProperty(i)) {
                        var listener = messageListeners[i];
                        listener.messageCreated(messageId, partner);
                    }
                }
            }
        }

        tokenChanged(token: string): void {
            this.logout();
            if (token) this.login(token);
        }
    }

}