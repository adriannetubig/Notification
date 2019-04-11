(function () {
    'use strict';
    angular
        .module('App')
        .factory('AuthenticatedService', AuthenticatedService);
    AuthenticatedService.$inject = ['$http', 'AuthenticationsFactory'];

    function AuthenticatedService($http, AuthenticationsFactory) {
        return {
            Send: Send
        };

        async function Send(notification) {
            var config = await AuthenticationsFactory.GetAuthenticatedConfig();
            return $http.post(URLSignalR + '/api/Notifications/SendMessageToAuthenticatedConsumer',
                notification, config);
        }
    }
})();