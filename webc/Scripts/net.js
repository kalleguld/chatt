var User = (function () {
    function User(username, fullName) {
        this.messages = [];
        this.lastMessage = 0;
        this.username = username;
        this.fullName = fullName;
    }
    User.prototype.addMessage = function (id, sent, outgoing, content) {
        var m = new Message(id, this.username, sent, outgoing, content);
        this.messages.push(m);
        this.lastMessage = Math.max(this.lastMessage, id);
        return m;
    };
    return User;
})();
var Message = (function () {
    function Message(id, partner, sent, outgoing, contents) {
        this.id = id;
        this.partner = partner;
        this.sent = sent;
        this.outgoing = outgoing;
        this.contents = contents;
        return this;
    }
    return Message;
})();
var ChattSingleton = (function () {
    function ChattSingleton() {
        this.friends = [];
        this.friendRequests = [];
        this.users = new Map();
        this.currentZIndex = 1;
    }
    return ChattSingleton;
})();
function classEscape(s) {
    return htmlEscape(s);
}
function classUnescape(s) {
    var p = document.createElement("p");
    p.innerHTML = s;
    var child = p.childNodes[0];
    return child ? child.nodeValue : "";
}
function htmlEscape(s) {
    var entityMap = {
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': "&quot;",
        "'": "&#39;",
        "/": "&#x2F;"
    };
    return String(s).replace(/[&<>"'\/]/g, function (ss) { return entityMap[ss]; });
}
function getUrl(path, params) {
    return "http://localhost:8733/jsonv1/" + path + encodeParams(params);
}
function encodeParams(map) {
    var result = "";
    var first = true;
    for (var key in map) {
        result += (first ? "?" : "&");
        first = false;
        result += encodeURIComponent(key) + "=" + encodeURIComponent(map[key]);
    }
    return result;
}
function getXhrErrorHandler($, helpText) {
    return function (xhr, errorType, error) {
        console.log("Xhr Error: " + helpText, xhr);
        if (errorType === "error") {
            var obj = $.parseJSON(xhr.responseText);
            if (!obj) {
                console.log("Unknown error: " + xhr.responseText);
            }
            if (obj.errorCode === 7) {
                console.log("incorrect login");
            }
        }
        else {
            console.log("error: " + errorType);
        }
    };
}
function getNewMessages($, token, user, callback) {
    $.ajax({
        url: getUrl("messages/", { token: token, sender: user.username, afterId: user.lastMessage }),
        error: getXhrErrorHandler($, " in getNewMessages"),
        type: "GET",
        success: function (result, status, xhr) {
            callback(result.messages);
        }
    });
}
//# sourceMappingURL=net.js.map