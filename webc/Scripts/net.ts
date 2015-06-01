
class User {
    username: string;
    fullName: string;
    messages: Array<Message> = [];
    lastMessage: number = 0;

    constructor(username: string, fullName: string) {
        this.username = username;
        this.fullName = fullName;
    }

    addMessage(id: number, sent: number, outgoing: boolean, content: string): Message {
        var m = new Message(id, this, sent, outgoing, content);
        this.messages.push(m);
        this.lastMessage = Math.max(this.lastMessage, id);
        return m;
    }
}

class Message {
    id: number;
    partner: User;
    sent: number;
    outgoing: boolean;
    content: string;

    constructor(id: number, partner: User, sent: number, outgoing: boolean, content: string) {
        this.id = id;
        this.partner = partner;
        this.sent = sent;
        this.outgoing = outgoing;
        this.content = content;
        return this;
    }
}

class ChattSingleton {
    friends: Array<string> = [];
    friendRequests: Array<string> = [];
    users: Array<User> = [];
    token: string = "931a9540-9238-4094-a03c-9b19219e2d97";
    self: User = null;
}

