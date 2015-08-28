
module dk.kalleguld.AngularChatt {

    var app = angular.module(AngularChatt.moduleName, ["ngRoute"]);

    //Services
    app.factory("RerestService",() =>
        new RerestService());

    app.factory("TokenService", [
        "$http",
        "$rootScope",
        "RerestService",
        (http, rootScope, rerest) =>
            new TokenService(http, rootScope, rerest)
    ]);

    app.factory("MessageListenerService", [
        "TokenService",
        (token) =>
            new MessageListenerService(token)
    ]);

    app.factory("MessageService", [
        "$http",
        "TokenService",
        "RerestService",
        (http, token, rerest) =>
            new MessageService(http, token, rerest)]);

    app.factory("UserService", [
        "$http",
        "TokenService",
        "RerestService",
        "MessageService",
        "MessageListenerService",
        (http, token, rerest, message, messageListener) =>
            new UserService(http, token, rerest, message, messageListener)
    ]);


    //Controllers
    app.controller("LoginController", [
        "$scope",
        "$rootScope",
        "$location",
        "TokenService",
        (scope, rootScope, loc, token) =>
            new LoginController(scope, rootScope, loc, token)
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
        (scope, rootScope, location, routeParams, timeout, user, token, message, mls) =>
            new MainController(scope, rootScope, location, routeParams, timeout, user, token, message, mls)
    ]);

    app.directive("scrollToBottom", [
        "$timeout",
        timeout => {
            return {
                link: ($scope, element, attrs) => {
                    $scope.$on("scrollDown", () => {
                        timeout(() => {
                            element[0].scrollTop = element[0].scrollHeight;
                        }, 0, false);
                    });
                }
            };
        }
    ]);
}
