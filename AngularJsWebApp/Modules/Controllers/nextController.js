(function () {

    'use strict';
    app.controller('nextController', ['$scope', 'AuthenticationService', function ($scope, authenticationService) {
        authenticationService.getUserInformation('6418baab-9f1d-4917-9ff6-33bdfc5c49cc');
        
    }]);
})();