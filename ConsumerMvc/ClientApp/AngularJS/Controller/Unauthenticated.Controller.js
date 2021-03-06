﻿(function () {
    'use strict';

    angular
        .module('App')
        .controller('UnauthenticatedController', UnauthenticatedController);

    UnauthenticatedController.$inject = ['$timeout', '$scope', 'UnauthenticatedService'];

    function UnauthenticatedController($timeout, $scope, UnauthenticatedService) {
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
            UnauthenticatedService.Send(vm.Notification)
                .then(function (response) {
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
                vm.Connection.on("UnauthorizedMessage", function (notification) {
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

        function Reconnect(iteration) {
            ConnectToSignalR();
            iteration += 1;
            if ((iteration < HubReconnectionAttempts || HubReconnectionAttempts === 0) && vm.Connection.state === 0)
                $timeout(function () { Reconnect(iteration); }, HubReconnectionAttemptDelaySeconds * iteration);
        }
    }
})();