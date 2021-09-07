
var app = angular.module('myApp', []);
addSpicyGeneralUtils(app);


var appScope = null;
app.controller('myCtrl', ['$scope', '$http', 'error_helper', '$spicy', '$sce',
    function ($scope, $http, error_helper, $spicy, $sce) {

        $scope.errorMessage = typeof $$errorMessage === 'undefined'? "" : $$errorMessage;


        $scope.formatAsHTML = function(v){
            return $sce.trustAsHtml(v);
        };


        appScope = $scope;

    }
]);


$(document).ready(function () {

   

});