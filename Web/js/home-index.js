//home-index.js
var module = angular.module("homeIndex", ['ngRoute']);

module.factory('dataService', function ($http, $q) {

    var _topics = [];

    var _isInit = false;

    var _isReady = function () {
        return _isInit;
    }

    var _getTopics = function () {

        var deferred = $q.defer();

        $http.get("/api/topics/?replies=true")
            .then(
            function (result) {
                //success
                angular.copy(result.data, _topics);
                _isInit = true;
                deferred.resolve();
            },
            function () {
                //error
                deferred.reject();
            });

        return deferred.promise;
    }

    var _addTopic = function (newTopic) {

        var deferred = $q.defer();

        $http.post('api/topics/', newTopic)
            .then(function (result) {
                //success
                var newlyCreatedTopic = result.data;
                _topics.splice(0, 0, newlyCreatedTopic);
                deferred.resolve(newlyCreatedTopic);
            },
            function () {
                //error
                deferred.reject();
            });

        return deferred.promise;
    }

    return {
        topics: _topics,
        getTopics: _getTopics,
        addTopic: _addTopic,
        isReady: _isReady
    };
});

module.controller("topicsController", function ($scope, $route, $http, dataService) {
    $scope.data = dataService;
    $scope.isBusy = false;
    $scope.$route = $route;

    if (dataService.isReady() == false) {

        $scope.isBusy = true;

        dataService.getTopics()
            .then(
            function (result) {
                //success
            },
            function () {
                //error
                console.log("esti cel mai smecher, ceva a mers naspa");
            })
            .then(
            function () {
                $scope.isBusy = false;
            });
    }

});
module.controller('newTopicController', function ($scope, $route, $http, $window, dataService) {
    $scope.newTopic = {};

    $scope.save = function () {

        dataService.addTopic($scope.newTopic)
            .then(
            function () {
                //success
                $window.location = '#/';
            },
            function () {
                //error
                console.log("cannot save data");
            });
    };
});

module.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/templates/topicsView.html',
            controller: 'topicsController',
        })
        .when('/newtopic', {
            templateUrl: '/templates/newTopicView.html',
            controller: 'newTopicController'
        })
        .otherwise({ redirectTo: '/' });
});
module.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

