var app = angular.module('app', []);

app.run(['$http', '$window', function ($http, $window) {
    $http.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    $http.defaults.headers.common['__RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();
}]);

app.service('appService', ['$http', function ($http) {
    this.GetAllBooks = function (o) {
        return $http.post('Book1/GetAllBooks', o);
    };
}]);

app.controller('IndexCtrl', ['$scope', '$window', 'appService', '$rootScope', function ($scope, $window, appService, $rootScope) {
    
    $scope.Books = [];
    appService.GetAllBooks({})
        .then(function (ret) {
            $scope.Books = ret.data;
        })
        .catch(function (ret) {
            alert('Error');
        });

    
}]);