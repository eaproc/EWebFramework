
var app = angular.module('myApp', []);
addSpicyGeneralUtils(app);


var appScope = null;
app.controller('myCtrl', ['$scope', '$http', 'error_helper', '$spicy',
    function ($scope, $http, error_helper, $spicy) {





        appScope = $scope;

    }
]);


$(document).ready(function () {

   

});