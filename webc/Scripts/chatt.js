window.chatt = new ChattSingleton();

function getToken(un, pw, callback) {
    $.ajax({
        url: getUrl("tokens/", { username: un, password: pw }),
        type: "POST",
        error: getXhrErrorHandler($, "in getToken"),
        success: callback
    });
}

function updateUserList(userList, token, url, callbackAdd, callbackRemove) {
    $.ajax({
        url: getUrl(url, { token: token }),
        type: "GET",
        error: getXhrErrorHandler($, "in updateUserList"),
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
        error: getXhrErrorHandler($, "in updateUserInfo for " + username),
        success: function (result, status, xhr) {
            var u = new User(result.username, result.fullName);
            window.chatt.users[u.username] = u;
            userList[u.username] = u;
            if (callback) callback(u);
        }
    });
}


