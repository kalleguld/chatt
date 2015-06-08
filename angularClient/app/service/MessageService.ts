module dk.kalleguld.AngularChatt {

    export class MessageService implements IMessageService {

        httpService: ng.IHttpService;
        tokenService: ITokenService;

        constructor($http: ng.IHttpService, tokenService: ITokenService) {
            this.httpService = $http;
            this.tokenService = tokenService;
        }

        getMessage(messageId: number): Message {
             throw new Error("Not implemented");
        }

        getMessages(user: User): Message[] {
             throw new Error("Not implemented");
        }
    }
}