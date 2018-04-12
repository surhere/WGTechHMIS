(function () {
    'use strict';
    app.service('LoginService', ['$http', '$q', 'AuthenticationService', 'authData',
    function ($http, $q, authenticationService, authData) {
        var userInfo;
        var loginServiceURL = serviceBase + 'api/Authenticate';
        var userInfoServiceURL = serviceBase + 'api/Admin';
        var deviceInfo = [];
        var deferred;

        this.login = function (userName, password) {
            debugger;
            deferred = $q.defer();
            var data = "grant_type=password&" + userName + ":" + password;
            $http.post(loginServiceURL, data, {
                headers:
                { "Authorization": "Basic " + btoa(userName + ":" + password) }
            }).success(function (response) {
                debugger;
                var o = response;
                userInfo = {
                    accessToken: response.access_token,
                    userName: response.userName
                };
                authenticationService.setTokenInfo(userInfo);
                authenticationService.setHeader($http);
                authData.authenticationData.IsAuthenticated = true;
                authData.authenticationData.userName = response.userName;
                deferred.resolve(null);
            })
            .error(function (err, status) {
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                deferred.resolve(err);
            });
            return deferred.promise;
        }

        this.getOrders = function (userID) {
            debugger;
            return $http.get(serviceBase + 'api/orders' + '?userid=' + userID).then(function (results) {
                return results;
            });
        };

        this.getUserInformation = function (userID) {
            debugger;
            deferred = $q.defer();
            $http.get(userInfoServiceURL + '?userid=' + userID, {              
            }).success(function (response) {
                debugger;
                var o = response;                 
                deferred.resolve(null);
            }).error(function (err, status) {                    
                    deferred.resolve(err);
                });
            return deferred.promise;
        }

        this.logOut = function () {
            authenticationService.removeToken();
            authData.authenticationData.IsAuthenticated = false;
            authData.authenticationData.userName = "";
        }
    }
    ]);
})();