module dk.kalleguld.AngularChatt {
    export class Routes {
        static configureRoutes(routeProvider: ng.route.IRouteProvider):void {

            routeProvider.when("/", {
                controller: "MainController",
                templateUrl: "/app/view/main.html",
                controllerAs: "ctrl"
            });

            routeProvider.when("/login", {
                controller: "LoginController",
                templateUrl: "/app/view/login.html",
                controllerAs: "ctrl"
            });

            routeProvider.otherwise({
                redirectTo: "/login"
            });
        }
    }
}