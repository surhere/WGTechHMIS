'use strict';
app.factory('authData', [ function () {
    var authDataFactory = {};

    var _authentication = {
        IsAuthenticated: false,
        userId: "",
        userName: "",
        firstName: "",
        lastName: "",
        tokenID: "",
        tokenExpiry: "",
        roles: [],
        permissions: [],
        hasOPDAllRights: false,
        hasIPDAllRights: false,
        hasAdminRights: false
    };
    authDataFactory.authenticationData = _authentication;

    return authDataFactory;
}]);