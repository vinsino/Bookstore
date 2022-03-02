var app = angular.module('app', []);

app.run(['$http', '$window', function ($http, $window) {
    $http.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    $http.defaults.headers.common['__RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();
}]);

app.service('appService', ['$http', function ($http) {

    this.CreateBook = function (o) {
        return $http.post('Book1/CreateBook', o);
    };
}]);

app.controller('CreateCtrl', ['$scope', '$window', 'appService', '$rootScope', function ($scope, $window, appService, $rootScope) {

    $scope.Book = {};

    $scope.CallCreateBook = function (){
        appService.CreateBook($scope.Book)
            .then(function (ret) {
            window.location.href = '/Book1/Index';
        });
        /*$window.location.href = '/Book1/Index';  */ 
    }

}]);