module dk.kalleguld.AngularChatt {
    export class Message {
        id: number;
        contents: string;
        outgoing: boolean;
        sent: Date;
    }
}