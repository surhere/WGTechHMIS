(function () {
    'use strict';
    app.controller('loginController', ['$scope', 'LoginService', '$location', function ($scope, loginService, $location) {

        $scope.loginData = {
            userName: "",
            password: ""
        };
        debugger;
        $scope.login = function () {
            loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {

                if (response != null && response.error != undefined) {
                    $scope.message = response.error_description;
                }
                else {
                    debugger;
                    var userdata = loginService.getUserInformation('6418baab-9f1d-4917-9ff6-33bdfc5c49cc').then(function (response) {
                        $scope.userinfo = 'TYestr'
                    });
                    $location.path('/next');
                }
            });
        }
    }]);
})();