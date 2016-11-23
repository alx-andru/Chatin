var chattin = angular.module('ChatinApp', [
	'officeuifabric.core',
    'officeuifabric.components',
    'ngSanitize',
    'ngResource'
    ]);

// Base URL for REST-API
var url = 'http://127.0.0.1:8085';

chattin.factory('Messages', function($resource, $window) {
	return $resource(url+'/api/rooms/:room/messages',{
	       room: '@room',
	    }, {
	    	inRoom: {
	            method: 'GET',
	            isArray: true,
	            params: {
	                room: 'string'
	            }
	        }
	    }
    ); 

});

chattin.factory('Rooms', function($resource, $window) {
	return $resource(url+'/api/rooms');
});    

chattin.factory('Users', function($resource, $window) {
	return $resource(url+'/api/users');
});    


chattin.controller('ChatinController', function($scope, Users, Rooms, Messages){

	// Initialize chat, based on server and in order.

	// Users
	$scope.users = Users.query(function(users){
		
		$scope.user = users[1]; // default user to Alex

		// Rooms
		$scope.rooms = Rooms.query(function(){			

    		$scope.activeRoom = $scope.rooms[0]; // default room to General

			// Messages
    		$scope.messages = Messages.inRoom({
				room: $scope.activeRoom.id,
			}, function(result){
				// console.log(result);
			});
    	});
	});


	// Toggle Rooms
	$scope.isOpen = false;

    $scope.joinRoom = function(idx) {
    	$scope.activeRoom = $scope.rooms[idx];
    	$scope.refresh();
    };

    $scope.toggleMenu = function () {
      $scope.isOpen = !$scope.isOpen;
    }

    // Refresh message list
    $scope.refresh = function(){
		$scope.messages = Messages.inRoom({
			room: $scope.activeRoom.id,
		});
	};

	// find current active user in list and return next.
    $scope.toggleUser = function () {
    	var position = 0;
    	for(var user in $scope.users){
    		if($scope.users[user] === $scope.user){
    			console.log(user);
    			position = parseInt(user)+1;
    		}
    	};
    	// user was last in list, switch to first user
    	var nextUser = position == $scope.users.length ? 0 : position; 
    	$scope.user= $scope.users[nextUser];
    }

    // determine if user is active user
	$scope.isUser = function(idx){
		return $scope.messages[idx].author === $scope.user.name;
	}

	// Textinput of new message
	$scope.msg = '';

	// Send the message
	$scope.send = function(){
		var text = $scope.msg;
		// if empty, ignore it
		if(text.length == 0){
			return;
		}

		var message = new Messages();
		message.text = text;
		message.author = $scope.user.id;
		message.room = $scope.activeRoom.id;

		message.$save().then(function(){
			// reload list to display new message
			$scope.refresh();
		});
	}

});


