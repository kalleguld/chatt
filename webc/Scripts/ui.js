﻿
function openChatWindowFromElement(elem) {
    var jelem = $(elem);
    var parent = jelem.parents(".friend-list-item");
    var forAttr = parent.attr("for");
    openChatWindow(forAttr);
    return false;
}

function setTopmost(elem) {
    elem.style.zIndex = ++window.chatt.currentZIndex;
}

function openChatWindow(username) {
    var user = window.chatt.users[username];
    var w;
    var existingWindow = $(".chat-window[for=" + username + "]");
    if (existingWindow.length > 0) {
        w = existingWindow;
    } else {
        w = getChatWindow(user);
        $("#chat-window-container").append(w);
        windowCreated(w);
    }
    setTopmost(w[0]);
    w.find(".message-input").focus();
}

function getChatWindow(user) {
    var w = $("#chat-window-template").clone();
    w.removeAttr("id");
    w.attr("for", classEscape(user.username));
    w.find(".username").html(htmlEscape(user.username));
    w.find(".full-name").html(htmlEscape(user.fullName));
    return w;
}

function showLoginWindow() {
    $("#login-window").show();
    $(".friend-window").hide();
    $(".friend-list .friend-list-item").remove();
    $(".chat-window-container .chat-window").remove();
}

function hideLoginWindow() {
    $("#login-window").hide();
    $(".friend-window").show();
}

function startLogin(username, password) {
    $.ajax({
        url: getUrl("tokens/", { username: username, password: password }),
        type: "POST",
        error: getXhrErrorHandler($, "in startLogin"),
        success: function (result, status, xhr) {
            window.chatt.token = result.token;
            hideLoginWindow();
            fetchFriendList();
            $.connection.hub.start().done(function () {
                $.connection.messageHub.server.login(window.chatt.token);
            });
        }
    });
}

function fetchFriendList() {
    $("#friend-list").empty();
    window.chatt.friends = [];
    $.ajax({
        url: getUrl("friends/", { token: window.chatt.token }),
        type: "GET",
        error: getXhrErrorHandler($, "updateFriendList"),
        success: function(result, status, xhr) {
            for (var i = 0; i < result.usernames.length ; i++) {
                var username = result.usernames[i];
                window.chatt.friends.push(username);
                fetchUser(username);
            }
        }
    });
}

function fetchUser(username) {
    var placeholder = getFriendItemPlaceholder(username);
    $(".friend-list").append(placeholder);
    $.ajax({
        url: getUrl("users/" + username, { token: window.chatt.token }),
        type: "GET",
        error: getXhrErrorHandler($, "in fetchFriend"),
        success: function(result, status, xhr) {
            var user = new User(result.username, result.fullName);
            window.chatt.users[result.username] = user;
            addToFriendList(user);
        }
    });
}

function addToFriendList(user) {
    var friendListItem = getFriendListItemElem(user);
    var friendLists = $(".friend-list");
    for (var i = 0; i < friendLists.length; i++) {
        var list = $(friendLists[i]);
        var placeholder = list.find(".friend-list-item[for=" + user.username + "]");
        if (placeholder.length > 0) {
            placeholder.replaceWith(friendListItem);
        } else {
            list.append(friendListItem);
        }
    }
}

function getFriendItemPlaceholder(username) {
    var elem = $("#friend-list-item-placeholder-template").clone();
    elem.removeAttr("id");
    elem.attr("for", username);
    setContents(elem, {
        username: username
    });
    return elem;
}

function getFriendListItemElem(user) {
    var elem = $("#friend-list-item-template").clone();
    elem.removeAttr("id");
    elem.attr("for", user.username);
    setContents(elem, {
        username: user.username,
        fullName: user.fullName
    });
    return elem;
}

function fetchMessage(messageId) {
    $.ajax({
        url: getUrl("messages/" + messageId, { token: window.chatt.token }),
        type: "GET",
        error: getXhrErrorHandler($, "fetchMessage"),
        success: function(result, status, xhr) {
            var newMessage = new Message(
                result.id,
                (result.outgoing ? result.receiver : result.sender),
                result.sent,
                result.outgoing,
                result.contents
            );
            displayMessage(newMessage);
        }
    });
}

function displayMessage(message) {
    var chatHistories = $(".chat-window[for=" + message.partner + "] .chat-history");
    var newItem = getMessageElem(message);
    for (var i = 0; i < chatHistories.length; i++) {
        var history = $(chatHistories[i]);

        var scrolledToBottom = isScrolledToBottom(history);
        var placeholder = history.find(".chat-history-item-placeholder-template[for=" + message.id + "]");
        if (placeholder.length > 0) {
            placeholder.replaceWith(newItem);
        } else {
            history.append(newItem);
        }
        if (scrolledToBottom) scrollToBottom(history);
    }
}

function scrollToBottom(jelem) {
    jelem[0].scrollTop = jelem[0].scrollHeight;
}

function isScrolledToBottom(jelem) {
    return jelem[0].scrollHeight - jelem[0].scrollTop == jelem.outerHeight();
}

function getMessageElem(message) {
    var elem = message.outgoing
        ? $("#chat-history-item-out-template")
        : $("#chat-history-item-in-template");
    elem = elem.clone();
    elem.removeAttr("id");
    elem.attr("for", message.id);
    var partner = window.chatt.users[message.partner];
    setContents(elem, {
        id: message.id,
        username: partner.username,
        fullName: partner.fullName,
        contents: message.contents,
        sent: message.sent
    });
    return elem;
}

function setContents(elem, contents) {
    for (key in contents) {
        elem.find("." + key).html(htmlEscape(contents[key]));
    }
}

function sendMessageFromElement(elem) {
    var parentWindow = $($(elem).parents(".chat-window")[0]);
    var targetUser = parentWindow.attr("for");
    var inputField = parentWindow.find(".message-input");
    var contents = inputField.val();
    sendMessage(targetUser, contents);
    inputField.val("");
    inputField.focus();
    return false;
}

function sendMessage(username, contents) {
    $.ajax({
        url: getUrl("messages/", {
            token: window.chatt.token,
            receiver: username,
            contents: contents
        }),
        type: "POST",
        error: getXhrErrorHandler($, "sendMessage")
    });
}

function closeWindow(containingElem) {
    var jelem = $(containingElem);
    var targetElem = (jelem.hasClass("closeable"))
        ? jelem
        : jelem.parents(".closeable");
    targetElem.remove();
}

function windowCreated(elem) {
    $(".window").draggable({ handle: ".window-header" });
    $(".resizable").resizable();
}