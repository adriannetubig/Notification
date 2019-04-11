(function () {
    'use strict';

    angular
        .module('App')
        .controller('AuthenticationsController', AuthenticationsController);

    AuthenticationsController.$inject = ['$cookies', 'AuthenticationsFactory', 'AuthenticationsService'];

    function AuthenticationsController($cookies, AuthenticationsFactory, AuthenticationsService) {
        var vm = this;

        vm.User = {
            Username: "username",
            Password: "password"
        };

        vm.Login = Login;

        function Login() {
            AuthenticationsService.Login(vm.User)
                .then(function (response) {
                    AuthenticationsFactory.StoreAuthentication(response.data);
                })
                .catch(function (error) {
                    console.error(error);
                });
        }
    }
})();