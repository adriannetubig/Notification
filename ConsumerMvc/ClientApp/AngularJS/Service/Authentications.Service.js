(function () {
    'use strict';
    angular
        .module('App')
        .factory('AuthenticationsService', AuthenticationsService);
    AuthenticationsService.$inject = ['$http'];

    function AuthenticationsService($http) {
        return {
            Login: Login,
            Refresh: Refresh
        };

        function Login(user) {
            return $http.post(URLAuthentication + '/api/v1/Authentications/Login',
                user);
        }

        function Refresh(authentication) {
            return $http.post(URLAuthentication + '/api/v1/Authentications/Refresh',
                authentication);
        }
    }
})();