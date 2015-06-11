module dk.kalleguld.AngularChatt {

    var app = angular.module(AngularChatt.moduleName);

    function authChecker($q, $rootScope, $location) {
        if (!!$rootScope.token) return true;
        $location.path("/login");
        return $q.reject();
    }

    //Routes
    app.config([
        "$routeProvider",
        (routeProvider) => {

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

            routeProvider.otherwise({
                redirectTo: "/login"
            });
        }
    ]);

}