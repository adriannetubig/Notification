(function () {
    'use strict';

    angular
        .module('App')
        .controller('AuthenticatedController', AuthenticatedController);

    AuthenticatedController.$inject = ['$timeout', '$scope', 'AuthenticatedService'];

    function AuthenticatedController($timeout, $scope, AuthenticatedService) {
        var vm = this;

        vm.Connection = null;
        vm.Notification = {
            Sender: 'AuthenticatedSender',
            Message: 'Message'
        };

        vm.Notifications = [];

        vm.Send = Send;

        vm.Initialise = Initialise;

        function Send() {
            AuthenticatedService.Send(vm.Notification)
                .then(function (response) {
                    console.log(vm.Notification);
                })
                .catch(function (error) {
                    console.error(error);
                });
        }

        function Initialise() {
            ConnectToSignalR();
        }

        function PushMessage(notification) {
            vm.Notifications.push(notification);
            $scope.$apply(); //We need this to update the UI
        }

        function ConnectToSignalR() {
            if (vm.Connection === null || vm.Connection.state === 0) {
                vm.Connection = new signalR.HubConnectionBuilder().withUrl(URLSignalR + '/unauthenticatedHub').build();
                vm.Connection.on("AuthorizedMessage", function (notification) {
                    PushMessage(notification);
                });
                //var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:40902/notificationHub", {
//    accessTokenFactory: () =>
//        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiam9obmRvZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE1NTM3NDE2MTUsImV4cCI6MTU1Mzc0MTY3NSwiaXNzIjoiSXNzdWVyIiwiYXVkIjoiQXVkaWVuY2UifQ.2HBSiii7sJAXg7dC55e5cKIUU1-TxYEAQphqzw0s85w"
//})
//    .build();
                vm.Connection.start().then(function () {
                }).catch(function (error) {
                    console.error(error);
                });

                vm.Connection.onclose(function () {
                    Reconnect(0);
                });
            }
        }

        function Reconnect(iteration) {
            ConnectToSignalR();
            iteration += 1;
            if ((iteration < HubReconnectionAttempts || HubReconnectionAttempts === 0) && vm.Connection.state === 0)
                $timeout(function () { Reconnect(iteration); }, HubReconnectionAttemptDelaySeconds * iteration);
        }
    }
})();