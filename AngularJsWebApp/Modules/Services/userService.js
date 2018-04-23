'use strict';
app.factory('userService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

   // var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var userServiceFactory = {};
    var _getOrders = function () {

        return $http.post(serviceBase + 'api/orders/?tokenid=' + u).then(function (results) {
            return results;
        });
    };

    ordersServiceFactory.getOrders = _getOrders;
    return ordersServiceFactory;

}]);
