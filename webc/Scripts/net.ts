
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
    contents: string;

    constructor(id: number, partner: User, sent: number, outgoing: boolean, content: string) {
        this.id = id;
        this.partner = partner;
        this.sent = sent;
        this.outgoing = outgoing;
        this.contents = content;
        return this;
    }
}

class ChattSingleton {
    friends: Array<string> = [];
    friendRequests: Array<string> = [];
    users: Map<string, User> = new Map<string, User>();
    token: string;
    currentZIndex: number = 1;
}

interface IXhrErrorHandler {
    (xhr: XMLHttpRequest, errorType: string, error: string): void
}

interface IMessageListHandler {
    (messageIds: Array<number>): any
}


function classEscape(s: string): string {
    return htmlEscape(s);
}
function classUnescape(s: string): string {
    var p = document.createElement("p");
    p.innerHTML = s;
    var child = p.childNodes[0];
    return child ? child.nodeValue : "";
}


function htmlEscape(s: string): string {
    var entityMap = {
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': "&quot;",
        "'": "&#39;",
        "/": "&#x2F;"
    };
    return String(s).replace(/[&<>"'\/]/g, ss => entityMap[ss]);
}

function getUrl(path: string, params: any): string {
    return "http://localhost:8733/jsonv1/" + path + encodeParams(params);
}

function encodeParams(map: Map<string, string>): string {
    var result = "";
    var first = true;
    for (var key in map) {
        result += (first ? "?" : "&");
        first = false;
        result += encodeURIComponent(key) + "=" + encodeURIComponent(map[key]);
    }
    return result;
}

function getXhrErrorHandler($, helpText: string): IXhrErrorHandler {
    return (xhr, errorType, error) => {
        console.log("Xhr Error: " + helpText, xhr);
        if (errorType === "error") {
            var obj = $.parseJSON(xhr.responseText);
            if (!obj) {
                console.log("Unknown error: " + xhr.responseText);
            }
            if (obj.errorCode === 7) {
                console.log("incorrect login");
            }
        } else {
            console.log("error: " + errorType);
        }
    }
}

function getNewMessages($, token: string, user: User, callback: IMessageListHandler) {
    $.ajax({
        url: getUrl("messages/", { token: token, sender: user.username, afterId: user.lastMessage }),
        error: getXhrErrorHandler($, " in getNewMessages"),
        type: "GET",
        success: (result, status, xhr) => {callback(result.messages)}
    });
}