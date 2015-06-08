
module dk.kalleguld.AngularChatt {

    var app = angular.module(AngularChatt.moduleName, ["ngRoute"]);

    app.factory("RerestService", [()=>new RerestService()]);

    app.factory("TokenService", [
        "$http",
        "RerestService",
        (http: ng.IHttpService, rerest: IRerestService) => new TokenService(http, rerest)
    ]);

    app.factory("MessageService", ["$http", "TokenService", MessageService]);

    app.controller("LoginController", [
        "$scope",
        "TokenService",
        ($scope, ts) => new LoginController($scope, ts)
    ]);

    app.config([
        "$routeProvider", routeProvider => {

            routeProvider.when("/", {
                controller: "MainController",
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
