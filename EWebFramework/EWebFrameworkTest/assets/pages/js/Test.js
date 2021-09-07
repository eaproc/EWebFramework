
var app = angular.module('myApp', ['Upload', 'stringFormatModule']);
addSpicyGeneralUtils(app);


var appScope = null;
app.controller('myCtrl', ['$scope', '$http', 'error_helper', '$spicy','Upload',
    function ($scope, $http, error_helper, $spicy, Upload) {






        appScope = $scope;

    }
]);


$(document).ready(function () {

   

});