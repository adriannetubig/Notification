(function () {
    'use strict';

    angular
        .module('App')
        .controller('AuthenticatedController', AuthenticatedController);

    AuthenticatedController.$inject = ['$timeout', '$scope', 'AuthenticatedService', 'AuthenticationsFactory'];

    function AuthenticatedController($timeout, $scope, AuthenticatedService, AuthenticationsFactory) {
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

        async function ConnectToSignalR() {
            if (vm.Connection === null || vm.Connection.state === 0) {
                var token = await AuthenticationsFactory.GetToken();
                vm.Connection = new signalR.HubConnectionBuilder().withUrl(URLSignalR + '/authenticatedHub', {
                    accessTokenFactory: () => token
                }).build();
                vm.Connection.on("AuthorizedMessage", function (notification) {
                    PushMessage(notification);
                });

                vm.Connection.start().then(function () {
                }).catch(function (error) {
                    console.error(error);
                });

                vm.Connection.onclose(function () {
                    Reconnect(0);
                });
            }
        }

        async function Reconnect(iteration) {
            await ConnectToSignalR();
            iteration += 1;
            if ((iteration < HubReconnectionAttempts || HubReconnectionAttempts === 0) && vm.Connection.state === 0)
                $timeout(function () { Reconnect(iteration); }, HubReconnectionAttemptDelaySeconds * iteration);
        }
    }
})();