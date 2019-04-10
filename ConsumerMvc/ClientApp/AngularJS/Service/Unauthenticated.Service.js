(function () {
    'use strict';
    angular
        .module('App')
        .factory('UnauthenticatedService', UnauthenticatedService);
    UnauthenticatedService.$inject = ['$http'];

    function UnauthenticatedService($http) {
        return {
            Send: Send
        };

        function Send(notification) {
            return $http.post(URLSignalR + '/api/Notifications/SendMessageToUnauthenticatedConsumer',
                notification);
        }
    }
})();