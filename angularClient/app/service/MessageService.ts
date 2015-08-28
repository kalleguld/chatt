module dk.kalleguld.AngularChatt {

    export class MessageService implements IMessageService {

        private _httpService: ng.IHttpService;
        private _tokenService: ITokenService;
        private _rerestService: IRerestService;

        constructor($http: ng.IHttpService, tokenService: ITokenService, rerestService:IRerestService) {
            this._httpService = $http;
            this._tokenService = tokenService;
            this._rerestService = rerestService;
        }

        getMessage(messageId: number): void {
             throw new Error("Not implemented");
        }

        getMessages(user: User): void {
            if (!this._tokenService.loggedIn) return;
            if (!user) return;

            var lastMessageId = (user.messages.length > 0
                ? user.messages[user.messages.length - 1].id
                : 0);

            var url = this._rerestService.getUrl("messages/", {
                sender: user.username,
                afterId: lastMessageId,
                maxResults: (lastMessageId > 0 ? -1 : 20)
            });

            this._httpService.get<IRMessageList>(url)
                .success((rMessageList: IRMessageList) => {
                    for (var i = 0; i < rMessageList.messages.length; i++) {
                        var rMessage = rMessageList.messages[i];
                        var message = new Message();
                        message.id = rMessage.id;
                        message.contents = rMessage.contents;
                        message.outgoing = rMessage.outgoing;
                        message.sent = new Date(rMessage.sent);
                        user.messages.push(message);
                        user.unreadMessages++;
                    }
            });
        }

        sendMessage(user: User): void {
            if (!this._tokenService.loggedIn) return;
            if (!user) return;
            if (user.outMessage.match(/^\s*$/g)) return;

            var url = this._rerestService.getUrl("messages/", {
                receiver: user.username,
                contents: user.outMessage
            });
            this._httpService.post(url, "")
                .success(() => {
                    user.outMessage = "";
                });
        }
    }

    interface IRMessageList {
        messages: Array<IRMessage>;
    }
    interface IRMessage {
        id: number;
        contents: string;
        outgoing: boolean;
        sender: string;
        receiver: string;
        sent: number;
    }
}