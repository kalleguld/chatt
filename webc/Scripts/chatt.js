window.chatt = {
    friends: {},
    friendRequests: {},
    users: {},
    token: "931a9540-9238-4094-a03c-9b19219e2d97",
    self: null
};

function User(username, fullName) {
    this.username = username;
    this.fullName = fullName;
    this.messages = [];
    this.lastMessage = 0;

    this.addMessage = function(id, sent, outgoing, content) {
        var m = new Message(id, this, sent, outgoing, content);
        this.messages.push(m);
        this.lastMessage = Math.max(this.lastMessage, id);
        return m;
    }
}

function Message(id, partner, sent, outgoing, content) {
    this.id = id;
    this.partner = partner;
    this.sent = sent;
    this.outgoing = outgoing;
    this.content = content;
}

function getToken(un, pw, callback) {
    $.ajax({
        url: getUrl("tokens/", { username: un, password: pw }),
        type: "POST",
        error: getXhrErrorHandler("in getToken"),
        success: callback
    });
}

function updateUserListElement(userList, userListElem, templateElem, idPrefix, token, url) {
    updateUserList(userList, token, url,
        function (user) {
            //callback for new users
            var e = templateElem.clone();
            e.attr("id", idPrefix + classEscape(user.username));
            e.attr("for", classEscape(user.username));
            e.find(".username").html(htmlEscape(user.username));
            e.find(".fullName").html(htmlEscape(user.fullName));

            userListElem.append(e);
        },
        function (username) {
            userListElem.find("#" + idPrefix + username)
                .remove();
        
    });
}

function updateUserList(userList, token, url, callbackAdd, callbackRemove) {
    $.ajax({
        url: getUrl(url, { token: token }),
        type: "GET",
        error: getXhrErrorHandler("in updateUserList"),
        success: function (result, status, xhr) {
            var newUsers = result.usernames;
            for (var i = 0; i < newUsers.length; i++) {
                var newUser = newUsers[i];
                if (!(newUser in userList)) {
                    updateUserInfo(newUser, userList, token, callbackAdd);
                }
            }
            for (var username in userList) {
                if (newUsers.indexOf(username) === -1) {
                    delete userList[username];
                    if (callbackRemove) callbackRemove(username);
                }
            }

        }
    });
}

function updateUserInfo(username, userList, token, callback) {
    $.ajax({
        url: getUrl("users/" + username + "/", { token: token }),
        type: "GET",
        error: getXhrErrorHandler("in updateUserInfo for " + username),
        success: function (result, status, xhr) {
            var u = new User(result.username, result.fullName);
            window.chatt.users[u.username] = u;
            userList[u.username] = u;
            if (callback) callback(u);
        }
    });
}

function sendChatMessage(token, receiver, content, callback) {
    $.ajax({
        url: getUrl("messages/", { token: token, receiver: receiver.username, content: content }),
        type: "POST",
        error: getXhrErrorHandler("in sendChatMessage"),
        success: function(result, status, xhr) {
            var m = receiver.addMessage(result.id, result.sent, true, result.content);
            if (callback) callback(receiver, m);
        }
    });
}

function classEscape(s) {
    return s;
}
function classUnescape(s) {
    return s;
}

function htmlEscape(s) {
    return s;
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

function getXhrErrorHandler(helpText) {
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
        } else {
            console.log("error: " + errorType);
        }
    }
}