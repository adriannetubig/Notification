(function () {
    'use strict';

    angular
        .module('App')
        .controller('UnauthenticatedController', UnauthenticatedController);

    UnauthenticatedController.$inject = ['$scope'];

    function UnauthenticatedController($scope) {
        var vm = this;

        vm.Connection = null;

        vm.Notifications = [];

        vm.Initialise = Initialise;

        function Initialise() {
            ConnectToSignalR();
        }

        function PushMessage(notification) {
            console.log(vm.Notifications);
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