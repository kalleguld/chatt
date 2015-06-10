
module dk.kalleguld.AngularChatt {

    var app = angular.module(AngularChatt.moduleName, ["ngRoute"]);

    //Services
    app.factory("RerestService", ()=>new RerestService());

    app.factory("TokenService", [
        "$http",
        "$rootScope",
        "RerestService",
        (http, rootScope, rerest) => new TokenService(http, rootScope, rerest)
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


    //Controllers
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

    function authChecker($q, $rootScope, $location) {
        if (!!$rootScope.token) return true;
        $location.path("/login");
        return $q.reject();
    }

    //Routes
    app.config([
        "$routeProvider",
        (routeProvider) => {

            routeProvider.when("/", {
                templateUrl: "app/view/main.html",
                resolve: {
                    authed: authChecker
                }
            });

            routeProvider.when("/login", {
                templateUrl: "app/view/login.html"
            });

            routeProvider.otherwise({
                redirectTo: "/login"
            });
        }
    ]);

}
