module dk.kalleguld.AngularChatt {

    export interface IMessageCreatedListener {
        messageCreated(messageId: number, partner: string): void
    }

    export interface IMessageListenerService {
        addListener(listener: IMessageCreatedListener): void;
        removeListener(listener: IMessageCreatedListener): void;
    }
}