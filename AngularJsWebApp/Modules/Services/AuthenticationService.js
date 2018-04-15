(function () {
    'use strict';
    app.service('AuthenticationService', ['$http', '$q', '$window', 'authData',
        function ($http, $q, $window, authData) {
            var tokenInfo;
            var userInfo;
            this.setTokenInfo = function (data) {
                debugger;
                tokenInfo = data;
                $window.sessionStorage["TokenInfo"] = JSON.stringify(tokenInfo);
              
            }

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                debugger;
                tokenInfo = null;
                $window.sessionStorage["TokenInfo"] = null;
            }

            this.setUserInfo = function (data) {
                debugger;
                userInfo = data;
                $window.sessionStorage["UserInfo"] = JSON.stringify(userInfo);
            }

            this.init = function () {
                if ($window.sessionStorage["TokenInfo"]) {
                    tokenInfo = JSON.parse($window.sessionStorage["TokenInfo"]
                        //get session user info
                    );                   
                }
                if ($window.sessionStorage["UserInfo"]) {
                    userInfo = JSON.parse($window.sessionStorage["UserInfo"]);
                    }
            }

            this.setHeader = function (http) {
                debugger;
                delete http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                    http.defaults.headers.common['Authorization'] = 'Token ' + tokenInfo.accessToken;
                    http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                    http.defaults.headers.common['Token'] =  tokenInfo.accessToken;
                }
            }

            this.validateRequest = function () {
                debugger;
                var url = serviceBase + 'api/authenticate';
                var deferred = $q.defer();
                $http.get(url).then(function () {
                    deferred.resolve(null);
                }, function (error) {
                    deferred.reject(error);
                });
                return deferred.promise;
            }          

            this.getUserInformation = function (userID) {
                debugger;
                var deferred = $q.defer();
                $http.get(serviceBase + 'api/admin' + '?id=' + userID, {                
                }).success(function (response) {
                    debugger;
                    var o = response;
                    $window.sessionStorage["UserInfo"] = JSON.stringify(response);
                    authData.authenticationData.IsAuthenticated = true;
                    //var userDataJSON = JSON.stringify(response);
                    //var objh = JSON.parse(userDataJSON);
                    authData.authenticationData.userName = response.user_name;
                    authData.authenticationData.firstName = response.first_name;
                    authData.authenticationData.lastName = response.last_name;
                    authData.authenticationData.roles = response.hmis_link_user_roles;
                    authData.authenticationData.userId = response.SID;
                    deferred.resolve(null);
                }).error(function (err, status) {
                    deferred.resolve(err);
                });
                return deferred.promise;
            }
            this.init();
        }
    ]);
})();