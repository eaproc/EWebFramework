
var app = angular.module('myApp', []);
addSpicyGeneralUtils(app);


var appScope = null;
app.controller('myCtrl', ['$scope', '$http', 'error_helper', '$spicy',
    function ($scope, $http, error_helper, $spicy) {


            $scope.trace_id = $$trace_id;



        appScope = $scope;

    }
]);


$(document).ready(function () {

   

});