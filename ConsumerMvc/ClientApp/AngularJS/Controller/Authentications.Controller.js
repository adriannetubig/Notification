(function () {
    'use strict';

    angular
        .module('App')
        .controller('AuthenticationsController', AuthenticationsController);

    AuthenticationsController.$inject = ['$cookies', 'AuthenticationsService'];

    function AuthenticationsController($cookies, AuthenticationsService) {
        var vm = this;

        vm.User = {
            Username: "username",
            Password: "password"
        };

        vm.Login = Login;

        function Login() {
            AuthenticationsService.Login(vm.User)
                .then(function (response) {
                    console.log(response.data);
                    $cookies.put('Authentication', JSON.stringify(response.data));
                    console.log(JSON.parse($cookies.get('Authentication')));
                })
                .catch(function (error) {
                    console.error(error);
                });
        }
    }
})();