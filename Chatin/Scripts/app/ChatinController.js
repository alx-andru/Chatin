var chatin = angular.module('ChatinApp', [
	'officeuifabric.core',
    'officeuifabric.components',
    'ngSanitize',
    'ngResource',
    'angularMoment'
]);


// Base URL for REST-API
var url = 'http://localhost:51852';
chatin.factory('Messages', function ($resource, $window) {
    return $resource(url + '/api/room/:room/messages', {
        room: '@roomid',
    }, {
        inRoom: {
            method: 'GET',
            isArray: true,
            params: {
                roomid: 'string'
            }
        }
    }
    );

});

chatin.factory('Rooms', function ($resource, $window) {
    return $resource(url + '/api/room');
});

chatin.factory('Users', function ($resource, $window) {
    return $resource(url + '/api/user');
});

chatin.factory('_', function ($window) {
    // Helper mixin to sort by keys and not just attributes
    $window._.mixin({
        'sortKeysBy': function (obj, comparator) {
            var keys = _.sortBy(_.keys(obj), function (key) {
                return comparator ? comparator(obj[key], key) : key;
            });

            return _.object(keys, _.map(keys, function (key) {
                return obj[key];
            }));
        }
    });
    return $window._;
});

chatin.directive('messagesBox', function ($timeout) {
    return {
        scope: {
            messages: '=messages',
            user: '=user',
            timestamps: '=timestamps'
        },
        templateUrl: '../Template/Message',
        link: function (scope, element) {
            scope.isUser = function(idx){
                console.log('user');
                return scope.messages[idx].user.name === scope.user.name;
            };

            // determine if messages can be grouped together
            // show only one timestamp for all the messages within 3min
            scope.displayTime = function (idx) {
                return _.find(scope.timestamps, function (item) {
                    return item == idx;
                });

            };
            
            scope.$watchCollection(scope.messages, function (newVal) {
                // delay scroll to ensure view is rendered
                $timeout(function () {
                    element[0].scrollTop = element[0].scrollHeight;
                },100);
            });

        }
    }
});

chatin.controller('ChatinController', function ($scope, Users, Rooms, Messages, moment) {

    $scope.displayTimestamps = [];

    // Initialize chat, based on server and in order.
    // Users
    $scope.users = Users.query(function (users) {

        $scope.user = users[1]; // default user to Alex

        // Rooms
        $scope.rooms = Rooms.query(function () {

            $scope.activeRoom = $scope.rooms[0]; // default room to General

            $scope.refresh();
        });
    });

    function determineActiveTimestamps(messages) {
        // console.log(result);
        var currentGroup;

        for (var msgIdx = messages.length - 1; msgIdx >= 0; msgIdx--) {
            var message = messages[msgIdx];
            var timestamp = moment(message.timestamp);

            // init for first iteration
            if (currentGroup == undefined) {
                currentGroup = timestamp;
                $scope.displayTimestamps.push(msgIdx);
            }

            // either fits in the group
            if (currentGroup.diff(timestamp, 'minutes') <= 3) {

            } else {
                // or create a new one
                currentGroup = timestamp;
                $scope.displayTimestamps.push(msgIdx);
            }
        }
    }

    // Toggle Rooms
    $scope.isOpen = false;

    $scope.joinRoom = function (idx) {
        $scope.activeRoom = $scope.rooms[idx];
        $scope.refresh();
    };

    $scope.toggleMenu = function () {
        $scope.isOpen = !$scope.isOpen;
    }

    // Refresh message list
    $scope.refresh = function () {
        // Messages
        $scope.messages = Messages.inRoom({
            room: $scope.activeRoom.id,
        }, function (messages) {
            determineActiveTimestamps(messages);
        });
    };

    // find current active user in list and return next.
    $scope.toggleUser = function () {
        var position = 0;
        for (var user in $scope.users) {
            if ($scope.users[user] === $scope.user) {
                position = parseInt(user) + 1;
            }
        };
        // user was last in list, switch to first user
        var nextUser = position == $scope.users.length ? 0 : position;
        $scope.user = $scope.users[nextUser];
    }


    

    // Textinput of new message
    $scope.msg = '';

    // Send the message
    $scope.send = function () {
        var text = $scope.msg;
        // if empty, ignore it
        if (text.length == 0) {
            return;
        }

        var message = new Messages();
        message.text = text;
        message.userid = $scope.user.id;
        message.roomid = $scope.activeRoom.id;

        message.$save().then(function () {
            // reload list to display new message
            $scope.refresh();
        });
    }

});


