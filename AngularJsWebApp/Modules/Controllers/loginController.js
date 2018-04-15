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
                    $location.path('/next');
                }
            });
        }
        $scope.getRight = function (id) {
            debugger;
            return loginService.getUserInformation(id)
                .then(function (response) {
                    $scope.userinfo = response.data;
                });
        };

        $scope.userlogin = function () {
            debugger;
            //$scope.login();
            $scope.getRight('6418baab-9f1d-4917-9ff6-33bdfc5c49cc');

            
        };
    }]);
})();