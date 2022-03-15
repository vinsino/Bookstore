var app = angular.module('app', []);

app.run(['$http', '$window', function ($http, $window) {
    $http.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    $http.defaults.headers.common['__RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();
}]);

app.service('appService', ['$http', function ($http) {

    this.GetBook = function (o) {
        return $http.post("Book1/GetBook", o);
    };

    this.DeleteBook = function (o) {
        return $http.post('Book1/DeleteBook', o);
    };
}]);

app.controller('DeleteCtrl', ['$scope', '$window', 'appService', '$rootScope', function ($scope, $window, appService, $rootScope) {

    $scope.Book = {};

    $scope.DeleteBook = function (){
        appService.DeleteBook({ id: $window.bookid })
            .then(function (ret) {
            $window.location.href = '/Book1/Index';
        });
        /*$window.location.href = '/Book1/Index'; */  
    }

    appService.GetBook({ id: $window.bookid })
        .then(function (ret) {
            $scope.Book = ret.data;
        });
}]);