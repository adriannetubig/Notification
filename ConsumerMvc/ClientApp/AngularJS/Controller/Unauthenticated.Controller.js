(function () {
    'use strict';

    angular
        .module('App')
        .controller('UnauthenticatedController', UnauthenticatedController);

    UnauthenticatedController.$inject = ['$scope', 'UnauthenticatedService'];

    function UnauthenticatedController($scope, UnauthenticatedService) {
        var vm = this;

        vm.Connection = null;
        vm.Notification = null;

        vm.Notifications = [];

        vm.Send = Send;

        vm.Initialise = Initialise;

        function Send() {
            UnauthenticatedService.Send(vm.Notification)
                .then(function (response) {
                })
                .catch(function (data) {
                    console.log(data);
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
            vm.Connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:40902/unauthenticatedHub").build();

            vm.Connection.on("AuthorizedMessage", function (notification) {
                PushMessage(notification);
            });

            vm.Connection.start().then(function () {
            }).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }
})();