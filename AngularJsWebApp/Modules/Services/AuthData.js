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
        roles:[]
    };
    authDataFactory.authenticationData = _authentication;

    return authDataFactory;
}]);