var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            AngularChatt.moduleName = "AngularChatt";
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var ChatWindowController = (function () {
                function ChatWindowController(scope) {
                    this._scope = scope;
                }
                Object.defineProperty(ChatWindowController.prototype, "messages", {
                    get: function () {
                        return this._scope.user.messages;
                    },
                    enumerable: true,
                    configurable: true
                });
                return ChatWindowController;
            })();
            AngularChatt.ChatWindowController = ChatWindowController;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var LoginController = (function () {
                function LoginController(scope, rootScope, location, tokenService) {
                    this.loginFailed = false;
                    this._scope = scope;
                    this._rootScope = rootScope;
                    this._location = location;
                    this._tokenService = tokenService;
                    rootScope.title = "Login";
                }
                Object.defineProperty(LoginController.prototype, "isLoggedIn", {
                    get: function () {
                        return this._tokenService.loggedIn;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(LoginController.prototype, "token", {
                    get: function () {
                        return this._tokenService.token;
                    },
                    enumerable: true,
                    configurable: true
                });
                LoginController.prototype.login = function () {
                    var _this = this;
                    this.loginFailed = false;
                    this._tokenService.setCredentials(this.username, this.password, function (success) {
                        if (success) {
                            _this._location.path("/chat");
                        }
                        else {
                            _this.loginFailed = true;
                        }
                    });
                };
                return LoginController;
            })();
            AngularChatt.LoginController = LoginController;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var MainController = (function () {
                function MainController(scope, rootScope, location, routeParams, timeout, us, ts, ms, mls) {
                    var _this = this;
                    this._scope = scope;
                    this._rootScope = rootScope;
                    this._loc = location;
                    this._routeParams = routeParams;
                    this._timeout = timeout;
                    this._messageListenerService = mls;
                    this._userService = us;
                    this._tokenService = ts;
                    this._messageService = ms;
                    scope.$on("$destroy", function () { _this.destructor(); });
                    this.parseRoute();
                    mls.addListener(this);
                    if (this._selectedUser) {
                        this.scrollDown();
                        $("#out-message").focus();
                    }
                }
                MainController.prototype.destructor = function () {
                    this._messageListenerService.removeListener(this);
                };
                MainController.prototype.parseRoute = function () {
                    if (this._routeParams.friend) {
                        if (this._userService.friends[this._routeParams.friend]) {
                            this._selectedUser = this._userService.friends[this._routeParams.friend];
                            this._rootScope.title = this._selectedUser.username;
                        }
                        else {
                            this._loc.path("/chat");
                        }
                    }
                    else {
                        this._rootScope.title = "Main";
                    }
                };
                MainController.prototype.scrollDown = function () {
                    var _this = this;
                    if (!this.selectedUser)
                        return;
                    this._timeout(function () {
                        _this._userService.markMessagesRead(_this._selectedUser);
                        _this._scope.$broadcast("scrollDown");
                        _this._scope.$apply();
                    }, 100, false);
                };
                Object.defineProperty(MainController.prototype, "friends", {
                    get: function () {
                        return this._userService.friends;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(MainController.prototype, "friendRequests", {
                    get: function () {
                        return this._userService.friendRequests;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(MainController.prototype, "loggedIn", {
                    get: function () {
                        return this._tokenService.loggedIn;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(MainController.prototype, "token", {
                    get: function () {
                        return this._tokenService.token;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(MainController.prototype, "username", {
                    get: function () {
                        return this._tokenService.username;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(MainController.prototype, "selectedUser", {
                    get: function () {
                        return this._selectedUser;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(MainController.prototype, "hasFriendRequests", {
                    get: function () {
                        for (var a in this.friendRequests) {
                            return true;
                        }
                        return false;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(MainController.prototype, "hasFriends", {
                    get: function () {
                        for (var a in this.friends) {
                            return true;
                        }
                        return false;
                    },
                    enumerable: true,
                    configurable: true
                });
                MainController.prototype.selectUser = function (user) {
                    this._loc.path("/chat/" + user.username);
                };
                MainController.prototype.updateHistory = function (user) {
                    this._messageService.getMessages(user);
                };
                MainController.prototype.sendMessage = function (user) {
                    this._messageService.sendMessage(user);
                    $("#out-message").focus();
                };
                MainController.prototype.prettyDate = function (d) {
                    var now = new Date();
                    var offsetMs = now.getDate() - d.getDate();
                    var oneDayMs = 1000 * 60 * 60 * 24;
                    if (offsetMs < oneDayMs) {
                        return d.toLocaleTimeString();
                    }
                    else {
                        return d.toLocaleDateString();
                    }
                };
                MainController.prototype.logout = function () {
                    this._tokenService.clearCredentials();
                    this._loc.path("/login");
                };
                MainController.prototype.messageCreated = function (messageId, partner) {
                    if (this._selectedUser && this._selectedUser.username === partner) {
                        this.scrollDown();
                    }
                };
                MainController.prototype.addFriend = function (user) {
                    this._userService.addFriend(user.username);
                };
                MainController.prototype.requestFriend = function () {
                    this._userService.addFriend(this.requestedFriend);
                    this.requestedFriend = "";
                };
                MainController.prototype.deleteFriendRequest = function (user) {
                    this._userService.deleteFriendRequest(user);
                };
                return MainController;
            })();
            AngularChatt.MainController = MainController;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var NewUserController = (function () {
                function NewUserController(userService, tokenService, locationService) {
                    this.username = "";
                    this.password1 = "";
                    this.password2 = "";
                    this.fullName = "";
                    this._takenUsername = "";
                    this._userService = userService;
                    this._tokenService = tokenService;
                    this._locationService = locationService;
                }
                Object.defineProperty(NewUserController.prototype, "showPasswordMismatchError", {
                    get: function () {
                        return this.password1 !== this.password2 &&
                            this.password1 !== "" &&
                            this.password2 !== "";
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(NewUserController.prototype, "usernameTaken", {
                    get: function () {
                        return this._takenUsername !== "" && this._takenUsername === this.username;
                    },
                    enumerable: true,
                    configurable: true
                });
                NewUserController.prototype.create = function () {
                    var _this = this;
                    if (this.password1 !== this.password2)
                        return;
                    if (this.username === "")
                        return;
                    this._userService.createUser(this.username, this.password1, this.fullName, function (u) {
                        _this._tokenService.setCredentials(_this.username, _this.password1, function (success) {
                            if (success) {
                                _this._locationService.path("/chat");
                            }
                        });
                    });
                };
                return NewUserController;
            })();
            AngularChatt.NewUserController = NewUserController;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            ;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var Message = (function () {
                function Message() {
                }
                return Message;
            })();
            AngularChatt.Message = Message;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var User = (function () {
                function User() {
                    this.unreadMessages = 0;
                    this.messages = new Array();
                }
                return User;
            })();
            AngularChatt.User = User;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var MessageListenerService = (function () {
                function MessageListenerService(token) {
                    this._messageListeners = [];
                    this._tokenService = token;
                    this._connection = $.connection;
                    this._connection.messageHub.client.newMessageCreated = this.getMessageHandler();
                    this._tokenService.addTokenChangeListener(this);
                    this.tokenChanged(this._tokenService.token);
                }
                MessageListenerService.prototype.addListener = function (listener) {
                    this._messageListeners.push(listener);
                };
                MessageListenerService.prototype.removeListener = function (listener) {
                    for (var i = this._messageListeners.length - 1; i >= 0; i--) {
                        var ml = this._messageListeners[i];
                        if (ml === listener) {
                            this._messageListeners.splice(i, 1);
                        }
                    }
                };
                MessageListenerService.prototype.login = function (token) {
                    var _this = this;
                    this._connection.hub.start().done(function () {
                        _this._connection.messageHub.server.login(token);
                    });
                };
                MessageListenerService.prototype.logout = function () {
                    this._connection.hub.stop();
                };
                MessageListenerService.prototype.getMessageHandler = function () {
                    var messageListeners = this._messageListeners;
                    return function (messageId, partner) {
                        for (var i in messageListeners) {
                            if (messageListeners.hasOwnProperty(i)) {
                                var listener = messageListeners[i];
                                listener.messageCreated(messageId, partner);
                            }
                        }
                    };
                };
                MessageListenerService.prototype.tokenChanged = function (token) {
                    this.logout();
                    if (token)
                        this.login(token);
                };
                return MessageListenerService;
            })();
            AngularChatt.MessageListenerService = MessageListenerService;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var MessageService = (function () {
                function MessageService($http, tokenService, rerestService) {
                    this._httpService = $http;
                    this._tokenService = tokenService;
                    this._rerestService = rerestService;
                }
                MessageService.prototype.getMessage = function (messageId) {
                    throw new Error("Not implemented");
                };
                MessageService.prototype.getMessages = function (user) {
                    if (!this._tokenService.loggedIn)
                        return;
                    if (!user)
                        return;
                    var lastMessageId = (user.messages.length > 0
                        ? user.messages[user.messages.length - 1].id
                        : 0);
                    var url = this._rerestService.getUrl("messages/", {
                        sender: user.username,
                        afterId: lastMessageId,
                        maxResults: (lastMessageId > 0 ? -1 : 20)
                    });
                    this._httpService.get(url)
                        .success(function (rMessageList) {
                        for (var i = 0; i < rMessageList.messages.length; i++) {
                            var rMessage = rMessageList.messages[i];
                            var message = new AngularChatt.Message();
                            message.id = rMessage.id;
                            message.contents = rMessage.contents;
                            message.outgoing = rMessage.outgoing;
                            message.sent = new Date(rMessage.sent);
                            user.messages.push(message);
                            user.unreadMessages++;
                        }
                    });
                };
                MessageService.prototype.sendMessage = function (user) {
                    if (!this._tokenService.loggedIn)
                        return;
                    if (!user)
                        return;
                    if (user.outMessage.match(/^\s*$/g))
                        return;
                    var url = this._rerestService.getUrl("messages/", {
                        receiver: user.username,
                        contents: user.outMessage
                    });
                    this._httpService.post(url, "")
                        .success(function () {
                        user.outMessage = "";
                    });
                };
                return MessageService;
            })();
            AngularChatt.MessageService = MessageService;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var RerestService = (function () {
                function RerestService() {
                }
                RerestService.prototype.getUrl = function (base, params) {
                    var result = RerestService.baseUrl;
                    result += base;
                    var first = true;
                    for (var p in params) {
                        result += (first ? "?" : "&");
                        first = false;
                        result += p + "=" + params[p];
                    }
                    return result;
                };
                RerestService.baseUrl = "http://localhost:8733/jsonv1/";
                return RerestService;
            })();
            AngularChatt.RerestService = RerestService;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var TokenService = (function () {
                function TokenService($http, rootScope, rerestService) {
                    this._username = null;
                    this._tokenChangeListeners = new Array();
                    this._http = $http;
                    this._rootScope = rootScope;
                    this._rerestService = rerestService;
                }
                Object.defineProperty(TokenService.prototype, "username", {
                    get: function () {
                        return this._username;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(TokenService.prototype, "token", {
                    get: function () {
                        return this._rootScope.token;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(TokenService.prototype, "pToken", {
                    set: function (token) {
                        var sendChange = (this._rootScope.token !== token);
                        this._rootScope.token = token;
                        if (token == null) {
                            this._http.defaults.headers.common.Authorization = undefined;
                        }
                        else {
                            this._http.defaults.headers.common.Authorization = "Token " + token;
                        }
                        if (sendChange) {
                            for (var i = 0; i < this._tokenChangeListeners.length; i++) {
                                this._tokenChangeListeners[i].tokenChanged(token);
                            }
                        }
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(TokenService.prototype, "loggedIn", {
                    get: function () {
                        return !!this.token;
                    },
                    enumerable: true,
                    configurable: true
                });
                TokenService.prototype.setCredentials = function (username, password, callback) {
                    var _this = this;
                    this.pToken = null;
                    this._username = null;
                    var url = this._rerestService.getUrl("tokens/", {
                        username: username,
                        password: password
                    });
                    this._http.post(url, {})
                        .success(function (result) {
                        _this.pToken = result.token;
                        _this._username = result.username;
                        if (callback)
                            callback(true);
                    })
                        .error(function () {
                        if (callback)
                            callback(false);
                    });
                };
                TokenService.prototype.clearCredentials = function () {
                    var url = this._rerestService.getUrl("tokens/", {});
                    this._http.delete(url);
                    this.pToken = null;
                    this._username = null;
                };
                TokenService.prototype.addTokenChangeListener = function (tcl) {
                    this._tokenChangeListeners.push(tcl);
                };
                TokenService.injName = "TokenService";
                return TokenService;
            })();
            AngularChatt.TokenService = TokenService;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var UserService = (function () {
                function UserService(http, tokenService, rerestService, ms, mls) {
                    this._friends = {};
                    this._friendRequests = {};
                    this._http = http;
                    this._tokenService = tokenService;
                    this._rerestService = rerestService;
                    this._messageService = ms;
                    this._messageListenerService = mls;
                    tokenService.addTokenChangeListener(this);
                    this.tokenChanged(tokenService.token);
                    mls.addListener(this);
                }
                Object.defineProperty(UserService.prototype, "friends", {
                    get: function () {
                        return this._friends;
                    },
                    enumerable: true,
                    configurable: true
                });
                Object.defineProperty(UserService.prototype, "friendRequests", {
                    get: function () {
                        return this._friendRequests;
                    },
                    enumerable: true,
                    configurable: true
                });
                UserService.prototype.updateFriends = function () {
                    var _this = this;
                    this._friends = {};
                    if (!this._tokenService.loggedIn) {
                        return;
                    }
                    var url = this._rerestService.getUrl("friends/", {});
                    this._http.get(url).success(function (data) {
                        var newFriends = {};
                        for (var i = 0; i < data.users.length; i++) {
                            var user = _this.parseRUser(data.users[i]);
                            newFriends[user.username] = null;
                            if (!(user.username in _this._friends)) {
                                _this._friends[user.username] = user;
                                _this._messageService.getMessages(user);
                            }
                        }
                        for (var username in _this._friends) {
                            if (!(username in newFriends)) {
                                delete _this._friends[username];
                            }
                        }
                    });
                };
                UserService.prototype.updateFriendRequests = function () {
                    var _this = this;
                    if (!this._tokenService.loggedIn) {
                        this._friendRequests = {};
                        return;
                    }
                    var url = this._rerestService.getUrl("friendrequests/", {});
                    this._http.get(url).success(function (data) {
                        var newRequests = {};
                        for (var i = 0; i < data.users.length; i++) {
                            var user = _this.parseRUser(data.users[i]);
                            newRequests[user.username] = null;
                            _this._friendRequests[user.username] = user;
                        }
                        for (var username in _this._friendRequests) {
                            if (!(username in newRequests)) {
                                delete _this._friendRequests[username];
                            }
                        }
                    });
                };
                UserService.prototype.addFriend = function (username) {
                    var _this = this;
                    var url = this._rerestService.getUrl("friendrequests/" + username + "/", {});
                    this._http.post(url, {})
                        .success(function (response) {
                        if (response.friendAdded) {
                            _this.updateFriends();
                            _this.updateFriendRequests();
                        }
                    });
                };
                UserService.prototype.parseRUser = function (rUser) {
                    var user = new AngularChatt.User();
                    user.username = rUser.username;
                    user.fullName = rUser.fullName;
                    return user;
                };
                UserService.prototype.tokenChanged = function (token) {
                    this.updateFriends();
                    this.updateFriendRequests();
                };
                UserService.prototype.messageCreated = function (messageId, partner) {
                    var user = this._friends[partner];
                    if (user)
                        this._messageService.getMessages(user);
                };
                UserService.prototype.markMessagesRead = function (user) {
                    user.unreadMessages = 0;
                };
                UserService.prototype.deleteFriendRequest = function (user) {
                    var _this = this;
                    var url = this._rerestService.getUrl("friendRequests/" + user.username + "/", {});
                    this._http.delete(url).success(function () {
                        _this.updateFriendRequests();
                    });
                };
                UserService.prototype.createUser = function (username, password, fullName, callback) {
                    var url = this._rerestService.getUrl("users/", {
                        username: username,
                        password: password,
                        fullName: fullName
                    });
                    this._http.post(url, {}).success(function (u) {
                        callback(u);
                    });
                };
                return UserService;
            })();
            AngularChatt.UserService = UserService;
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var app = angular.module(AngularChatt.moduleName, ["ngRoute"]);
            app.factory("RerestService", function () {
                return new AngularChatt.RerestService();
            });
            app.factory("TokenService", [
                "$http",
                "$rootScope",
                "RerestService",
                function (http, rootScope, rerest) {
                    return new AngularChatt.TokenService(http, rootScope, rerest);
                }
            ]);
            app.factory("MessageListenerService", [
                "TokenService",
                function (token) {
                    return new AngularChatt.MessageListenerService(token);
                }
            ]);
            app.factory("MessageService", [
                "$http",
                "TokenService",
                "RerestService",
                function (http, token, rerest) {
                    return new AngularChatt.MessageService(http, token, rerest);
                }]);
            app.factory("UserService", [
                "$http",
                "TokenService",
                "RerestService",
                "MessageService",
                "MessageListenerService",
                function (http, token, rerest, message, messageListener) {
                    return new AngularChatt.UserService(http, token, rerest, message, messageListener);
                }
            ]);
            app.controller("LoginController", [
                "$scope",
                "$rootScope",
                "$location",
                "TokenService",
                function (scope, rootScope, loc, token) {
                    return new AngularChatt.LoginController(scope, rootScope, loc, token);
                }
            ]);
            app.controller("NewUserController", [
                "UserService",
                "TokenService",
                "$location",
                function (userService, tokenService, locationService) {
                    return new AngularChatt.NewUserController(userService, tokenService, locationService);
                }
            ]);
            app.controller("MainController", [
                "$scope",
                "$rootScope",
                "$location",
                "$routeParams",
                "$timeout",
                "UserService",
                "TokenService",
                "MessageService",
                "MessageListenerService",
                function (scope, rootScope, location, routeParams, timeout, user, token, message, mls) {
                    return new AngularChatt.MainController(scope, rootScope, location, routeParams, timeout, user, token, message, mls);
                }
            ]);
            app.directive("scrollToBottom", [
                "$timeout",
                function (timeout) {
                    return {
                        link: function ($scope, element, attrs) {
                            $scope.$on("scrollDown", function () {
                                timeout(function () {
                                    element[0].scrollTop = element[0].scrollHeight;
                                }, 0, false);
                            });
                        }
                    };
                }
            ]);
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
var dk;
(function (dk) {
    var kalleguld;
    (function (kalleguld) {
        var AngularChatt;
        (function (AngularChatt) {
            var app = angular.module(AngularChatt.moduleName);
            function authChecker($q, $rootScope, $location) {
                if (!!$rootScope.token)
                    return true;
                $location.path("/login");
                return $q.reject();
            }
            app.config([
                "$routeProvider",
                function (routeProvider) {
                    routeProvider.when("/", {
                        redirectTo: "/chat"
                    });
                    routeProvider.when("/chat", {
                        templateUrl: "app/view/main.html",
                        resolve: {
                            authed: authChecker
                        }
                    });
                    routeProvider.when("/chat/:friend", {
                        templateUrl: "app/view/main.html",
                        resolve: {
                            authed: authChecker
                        }
                    });
                    routeProvider.when("/login", {
                        templateUrl: "app/view/login.html"
                    });
                    routeProvider.when("/newUser", {
                        templateUrl: "app/view/newUser.html"
                    });
                    routeProvider.otherwise({
                        redirectTo: "/login"
                    });
                }
            ]);
        })(AngularChatt = kalleguld.AngularChatt || (kalleguld.AngularChatt = {}));
    })(kalleguld = dk.kalleguld || (dk.kalleguld = {}));
})(dk || (dk = {}));
//# sourceMappingURL=ts.js.map