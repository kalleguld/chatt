
function openChatWindowFromElement(elem) {
    var fli = $(elem).parents(".friend-list-item")[0];
    var escUsername = $(fli).attr("for");
    openChatWindow(classUnescape(escUsername));
    return false;
}

function openChatWindow(username) {
    var user = window.chatt.users[username];
    var w = $("#chat-window-template").clone();
    w.attr("id", "m-cw-" + classEscape(username));
    w.attr("for", classEscape(username));
    w.find(".username").html(htmlEscape(username));
    w.find(".full-name").html(htmlEscape(user.fullName));

    $("#chat-window-container").append(w);
}

function updateFriendList() {
    var idPrefix = "m-fli-";
    updateUserList(window.chatt.friends,
        window.chatt.token,
        "friends/",
        function (user) {
            //callback for added users
            var fli = $("#friend-list-item-template").clone();
            fli.attr("id", idPrefix + user.username);
            fli.attr("for", classEscape(user.username));
            fli.find(".username").html(htmlEscape(user.username));
            fli.find(".full-name").html(htmlEscape(user.fullName));
            $("#friend-list").append(fli);
        },
        function (username) {
            //callback for removed users
            $("#" + idPrefix + classEscape(username)).remove();
        })
}

function sendChatMessageForm(form) {
    var chatWindow = $($(form).parents(".chat-window")[0]);
    var username = classUnescape(chatWindow.attr("for"));
    var user = window.chatt.users[username];

    sendChatMessage(window.chatt.token,
        user,
        $(form).find("[name=chat-input]").val(),
        function () { updateChatWindow(chatWindow) });
    return false;
}


function updateChatWindow(jElem) {
    var chatHistory = jElem.find(".chat-history");
    var username = classUnescape(jElem.attr("for"));
    var user = window.chatt.users[username];
    var token = window.chatt.token;
    getNewMessages($, token, user, function(messageNumbers) {
        for (var i = 0; i < messageNumbers.length; ++i) {
            var messageNumber = messageNumbers[i];
            insertMessage(token, user, messageNumber, chatHistory);
        }
    });
}

function insertMessage(token, user, messageNumber, chatHistory) {
    
    var placeholder = $("#chat-history-item-placeholder-template").clone();
    placeholder.attr("id", "");
    chatHistory.append(placeholder);
    $.ajax({
        url: getUrl("messages/" + messageNumber + "/", {
            token: token,
            afterId: user.lastMessage,
            sender: user.username
        }),
        type: "GET",
        error: [
            getXhrErrorHandler($, "getting message number " + messageNumber),
            function (a,b,c){placeholder.remove()}],
        success: function (result, status, xhr) {
            var msg = user.addMessage(result.id, result.sent, result.outgoing, result.content);
            var historyItem;
            if (msg.outgoing) {
                historyItem = $("#chat-history-item-out-template").clone();
            } else {
                historyItem = $("#chat-history-item-in-template").clone();
            }
            historyItem.attr("id", "m-chi-" + classEscape(msg.id));
            historyItem.attr("for", classEscape(msg.id));
            historyItem.find(".username").html(htmlEscape(user.username));
            historyItem.find(".full-name").html(htmlEscape(user.fullName));
            historyItem.find(".content").html(htmlEscape(msg.content));
            historyItem.find(".message-id").html(htmlEscape(msg.id));
            placeholder.after(historyItem);
            placeholder.remove();
        }
    });
}