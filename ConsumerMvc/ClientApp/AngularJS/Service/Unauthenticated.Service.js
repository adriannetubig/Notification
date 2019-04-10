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
            return $http.post('http://localhost:40902/api/Notifications/SendMessageToUnauthenticatedConsumer',
                notification);
        }
    }
})();