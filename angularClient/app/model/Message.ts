module dk.kalleguld.AngularChatt {
    export class Message {
        id: number;
        contents: string;
        partner: User;
        outgoing: boolean;
        sent: Date;
    }
}