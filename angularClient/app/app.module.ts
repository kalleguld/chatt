
module dk.kalleguld.AngularChatt {

    var app = angular.module(AngularChatt.moduleName, ["ngRoute"]);

    app.factory("RerestService", ()=>new RerestService());

    app.factory("TokenService", [
        "$http",
        "RerestService",
        (http, rerest) => new TokenService(http, rerest)
    ]);

    app.factory("MessageService", [
        "$http",
        "TokenService",
        "RerestService",
        (http, token, rerest) => new MessageService(http, token, rerest)]);

    app.factory("UserService", [
        "$http", "TokenService", "RerestService", "MessageService",
        (http, token, rerest, message) =>
            new UserService(http, token, rerest, message)
    ]);

    app.controller("LoginController", [
        "$scope",
        "$location",
        "TokenService",
        (scope, loc, token) => new LoginController(scope, loc, token)
    ]);

    app.controller("MainController", [
        "$scope",
        "UserService",
        "TokenService",
        "MessageService",
        (scope, user, token, message)=>new MainController(scope, user, token, message)
    ]);

    app.config([
        "$routeProvider", routeProvider => {

            routeProvider.when("/", {
                templateUrl: "app/view/main.html"
            });

            routeProvider.when("/login", {
                templateUrl: "app/view/login.html",
                controller: "LoginController"
            });

            routeProvider.otherwise({
                redirectTo: "/login"
            });
        }
    ]);
}
