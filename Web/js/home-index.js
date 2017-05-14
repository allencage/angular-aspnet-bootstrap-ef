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

        $http.post('/api/topics/', newTopic)
            .then(
            function (result) {
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

    function _findTopic(id) {
        var found = null;

        $.each(_topics, function (i, item) {
            if (item.id == id)
                found = item;
            return found;
        });

        return found;
    }

    function _returnTopic(deferred, id) {
        //var topic = _findTopic(id);
        //if (topic) {
        //    deferred.resolve(topic);
        //} else {
        //    deferred.reject();
        //}
    }

    var _getTopicById = function (id) {

        var deferred = $q.defer();

        if (_isReady()) {
            var topic = _findTopic(id);
            if (topic) {
                deferred.resolve(topic);
            } else {
                deferred.reject();
            }
        }
        else {
            _getTopics()
                .then(function () {
                    //success
                    var topic = _findTopic(id);
                    if (topic) {
                        deferred.resolve(topic);
                    } else {
                        deferred.reject();
                    }
                },
                function () {
                    //error
                    deferred.reject();
                })
        }

        return deferred.promise;
    }

    var _saveReply = function (topic, newReply) {

        var deferred = $q.defer();

        $http.post("/api/topics/" + topic.id + "/replies", newReply)
            .then(
            function (result) {
                //success
                if (topic.replies == null) topic.replies = [];
                topic.replies.push(result.data);
                deferred.resolve(result.data);
            },
            function () {
                deferred.reject();
            });

        return deferred.promise;
    }

    return {
        topics: _topics,
        getTopics: _getTopics,
        addTopic: _addTopic,
        isReady: _isReady,
        getTopicById: _getTopicById,
        saveReply: _saveReply
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

module.controller('singleTopicController', function ($scope, dataService, $window, $routeParams) {

    $scope.topic = null;
    $scope.newReply = {};
    //it knows about the id because in the routeProvider I specified the id as the parameter
    dataService.getTopicById($routeParams.id)
        .then(
        function (topic) {
            //success
            $scope.topic = topic;
        },
        function () {
            //error <- there is something wrong with this shit, it gets executed all the time...!
            $window.location = "#/";
        });

    $scope.addReply = function () {
        dataService.saveReply($scope.topic, $scope.newReply)
            .then(
            function () {
                $scope.newReply.body = "";
            },
            function () {
                alert("could not save reply");
            });
    }

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
        .when('/newtopic/:id', {
            templateUrl: '/templates/singleTopicView.html',
            controller: 'singleTopicController'
        })
        .otherwise({ redirectTo: '/' });
});

module.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

