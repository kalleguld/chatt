
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
        function(user) {
            //callback for added users
            var fli = $("#friend-list-item-template").clone();
            fli.attr("id", idPrefix + user.username);
            fli.attr("for", classEscape(user.username));
            fli.find(".username").html(htmlEscape(user.username));
            fli.find(".full-name").html(htmlEscape(user.fullName));
            $("#friend-list").append(fli);
        },
        function(username) {
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
        function (r, m) {
            var w = $("#chat-history-item-out-template").clone();
            w.attr("id", "m-chi-" + classEscape(r.username));
            w.attr("for", classEscape(r.username));
            w.find(".username").html(htmlEscape(r.username));
            w.find(".full-name").html(htmlEscape(r.fullName));
            w.find(".content").html(htmlEscape(m.content));
            w.find(".message-id").html(htmlEscape(m.id));
            chatWindow.find(".chat-history").append(w);
        });
    return false;
}