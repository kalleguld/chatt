module dk.kalleguld.AngularChatt {

    export interface IMessageService {
        getMessage(messageId: number): void;
        getMessages(user: User): void;
        sendMessage(user:User):void;
    }
}