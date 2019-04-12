(function () {
    'use strict';
    angular
        .module('App')
        .factory('AuthenticationsFactory', AuthenticationsFactory);
    AuthenticationsFactory.$inject = ['$cookies', 'AuthenticationsService'];

    function AuthenticationsFactory($cookies, AuthenticationsService) {
        return {
            GetAuthenticatedConfig: GetAuthenticatedConfig,
            GetToken: GetToken,
            StoreAuthentication: StoreAuthentication
        };

        async function GetAuthenticatedConfig() {
            var token = await GetToken();
            var config =
            {
                headers:
                {
                    'Authorization': 'Bearer ' + token
                }
            };
            return config;
        }

        async function GetToken() {
            var authentication = JSON.parse($cookies.get('NotificationAuthentication'));
            if (!authentication) {
                console.error('Invalid Authentication please log in again');
                return null;
            }

            var now = new Date();
            var expiration = new Date(authentication.expiration);
            if (expiration < now) {
                return await AuthenticationsService.Refresh(authentication)
                    .then(function (response) {
                        authentication = response.data;
                        StoreAuthentication(authentication);
                        authentication = authentication;
                        return authentication.token;
                    })
                    .catch(function (error) {
                        console.error(error);
                    });
            }

            return authentication.token;
        }

        function StoreAuthentication(authentication) {
            $cookies.put('NotificationAuthentication', JSON.stringify(authentication));
        }
    }
})();