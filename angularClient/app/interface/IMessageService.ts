module dk.kalleguld.AngularChatt {

    export interface IMessageService {
        getMessage(messageId: number): Message;
        getMessages(user:User): Array<Message>;
    }
}