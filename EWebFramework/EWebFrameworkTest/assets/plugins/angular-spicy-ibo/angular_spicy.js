/**
 * Created by ibo on 20.03.2017.
 */

/*
|--------------------------------------------------------------------------
| Angular Spicy 1.*
|--------------------------------------------------------------------------
| Make sure you add angular script before including this file
|
*/

/**
 * Allows the conversion of string ng-model to number
 * @param app angular module
 */
function directive_parseStringModelToNumber(app) {
    app.directive('input', [function() {
        return {
            restrict: 'E',
            require: '?ngModel',
            link: function(scope, element, attrs, ngModel) {
                if (
                    'undefined' !== typeof attrs.type
                    && 'number' === attrs.type
                    && ngModel
                ) {
                    ngModel.$formatters.push(function(modelValue) {
                        return Number(modelValue);
                    });

                    ngModel.$parsers.push(function(viewValue) {
                        return Number(viewValue);
                    });
                }
            }
        }
    }]);
}


/**
 * Use to convert all tags model string value to int
 * @param app angular
 */
function directive_parseStringModelToNumberAllTags(app) {
    app.directive('stringToNumber', function() {
        return {
            require: 'ngModel',
            link: function(scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function(value) {
                    return '' + value;
                });
                ngModel.$formatters.push(function(value) {
                    return parseFloat(value);
                });
            }
        };
    });
}



/**
 * Use to read single file into the model
 * @param app angular
 */
function directive_FileReadModel(app) {
    app.directive("fileread", [function () {
        return {
            scope: {
                fileread: "="
            },
            link: function (scope, element, attributes) {
                element.bind("change", function (changeEvent) {
                    var reader = new FileReader();
                    reader.onload = function (loadEvent) {
                        scope.$apply(function () {
                            scope.fileread = loadEvent.target.result;
                        });
                    }
                    reader.readAsDataURL(changeEvent.target.files[0]);
                });
            }
        }
    }]);

}




/**
 |
 |  You should include angular js before this file
 |
 **/
angular.module('stringFormatModule', [])
    .filter('titleCase', function () {
        return function (input) {
            input = input || '';
            return input.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
        };
    });

angular.module('numberFormatModule', [])
    .filter('numberFixedLen', function () {
        return function (n, len) {
            var num = parseInt(n, 10);
            len = parseInt(len, 10);
            if (isNaN(num) || isNaN(len)) {
                return n;
            }
            num = '' + num;
            while (num.length < len) {
                num = '0' + num;
            }
            return num;
        };
    });



/*
|--------------------------------------------------------------------------
| General Utils
|--------------------------------------------------------------------------
| Combination of several utils
|
*/
/**
 * This will add general util service to your angular instance.
 * Add to controller: app.controller('myCtrl', ['$scope','$spicy', function ($scope,$spicy)
 * @param app
 */
function addSpicyGeneralUtils(app) {


    /**
     *  This helps to fix switchBootstrap UI not updating issue
     */
    app.directive('input', ['$parse', function($parse) {
        return {
            restrict: 'E',
            require: '?ngModel',
            link: function(scope, element, attrs, ngModel) {
                if (
                    'undefined' !== typeof attrs.class
                    && attrs.class.indexOf("switchBootstrap")>=0
                    && ngModel
                ) {

                    ngModel.$formatters.push(function(modelValue) {
                        // console.log("modelValue: " + (typeof modelValue));

                        if(modelValue !== undefined && modelValue!== null)
                        {

                            var e = $(element);
                            if(e.bootstrapSwitch('state')!== modelValue)
                            {
                                // run this after modelValue has been set
                                setTimeout(function(){
                                    e.trigger("change");
                                },200);
                            }
                        }

                        return modelValue;
                    });

                    ngModel.$parsers.push(function(viewValue) {


                        return viewValue;
                    });
                }
            }
        }
    }]);





    /**
     *  This helps to activate date picker on input element and converts its value from Epoch format /Date(44444)/ to string "02/07/2018"
     *  Usage: <input date-format="date-picker">  : add the attribute to the input object
     */
    app.directive('input', ['$parse', function($parse) {
        return {
            restrict: 'E',
            require: '?ngModel',
            link: function(scope, element, attrs, ngModel) {
                if (
                    'undefined' !== typeof attrs.format
                    && 'date-picker' === attrs.format
                    && ngModel
                ) {

                    var id = attrs.id;

                    ngModel.$formatters.push(function(modelValue) {
                        if(modelValue===undefined || modelValue === null )
                        {
                            return "";
                        }

                        if(modelValue.toString().startsWith("/Date("))
                        {
                            var momentFormatted = moment(modelValue.toString()).format(___CLIENT___FILTER___DISPLAY_DATE_FORMAT);
                            // ngModel.$setViewValue(momentFormatted);

                            // $parse works out how to get the value.
                            // This returns a function that returns the result of your ng-model expression.
                            var modelGetter = $parse(attrs['ngModel']);
                            // console.log(modelGetter(scope));

                            // This returns a function that lets us set the value of the ng-model binding expression:
                            var modelSetter = modelGetter.assign;

                            // This is how you can use it to set the value 'bar' on the given scope.
                            modelSetter(scope, momentFormatted);

                            // console.log(modelGetter(scope));

                            return momentFormatted;
                        }

                        // don't understand or already converted
                        return modelValue;
                    });

                    ngModel.$parsers.push(function(viewValue) {
                        if(viewValue===undefined || viewValue === null ) return "";
                        if(viewValue.toString().startsWith("/Date("))
                        {
                            return moment(viewValue.toString()).format(___CLIENT___FILTER___DISPLAY_DATE_FORMAT);
                        }
                        // don't understand or already converted
                        return viewValue;
                    });
                }
            }
        }
    }]);



    /**
     *  This helps to convert select model value and view to string. Also, eliminates null and undefine as empty string
     *  Usage: <select data-type="string">  : add the attribute to the select object
     */
    app.directive('select', [function() {
        return {
            restrict: 'E',
            require: '?ngModel',
            link: function(scope, element, attrs, ngModel) {
                if (
                    'undefined' !== typeof attrs.type
                    && 'string' === attrs.type
                    && ngModel
                ) {
                    ngModel.$formatters.push(function(modelValue){
                        result = modelValue===undefined || modelValue === null ? "" : modelValue.toString();

                        // this is to fix select2 bug not updating view on model data changed
                        if(attrs.class && attrs.class.indexOf("select2")>=0)
                            $(element).val(result).trigger('change.select2');

                        return result;
                    });

                    ngModel.$parsers.push(function(viewValue) {
                        return viewValue===undefined || viewValue === null ? "" : viewValue.toString();
                    });
                }
            }
        }
    }]);


    /**
     * This is for string transformation
     */
    app.directive('input', [function() {
        return {
            restrict: 'E',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (
                    'undefined' !== typeof attrs.type
                    && 'text' === attrs.type
                    &&   'undefined' !== typeof attrs.textTransform
                    && ngModel
                ) {


                    ngModel.$formatters.push(function (modelValue) {

                        if(attrs.textTransform === 'uppercase')
                           return toStr(modelValue).toUpperCase();
                        if(attrs.textTransform === 'title-case')
                            return toTitleCase(modelValue);
                        return modelValue;
                    });

                    ngModel.$parsers.push(function (viewValue) {

                        if(attrs.textTransform === 'uppercase')
                            viewValue = toStr(viewValue).toUpperCase();
                        if(attrs.textTransform === 'title-case')
                            viewValue = toTitleCase(viewValue);


                        // update real control
                        // ngModel.$setViewValue(viewValue);
                        // ngModel.$render();

                        return viewValue;

                    });


                }
            }
        }}]);



    /**
     * Service for invoke array errors as string
     * @param app
     */
    app.service('error_helper', function() {

        this.combineMessageWithDataErrors = function (responseData) {
            var msg = responseData.message? responseData.message: "";
            if(responseData.data)
                msg += "\n\nDetails:\n=======================================\n"
                    + this.compactForHTML( responseData.data);
            return msg;
        };

        this.compactForHTML = function ($pErrorObject) {
            $pMsg="";
            angular.forEach($pErrorObject, function($value, $key) {
                $pMsg += ($value) + "<br/>";
            });

            //console.log((array) $pErrorObject);
            // Usage
            // -------------------------------------------------------------------------------------
            //  app.controller('myCtrl', ['$scope','errorhelper', function ($scope,errorhelper)
            // errorhelper.compactForHTML(response.data.data.errors);
            //
            return $pMsg;
        }
    });


    app.service('$spicy', function() {


        /**
         * Runs the function with the digest|apply scope if necessary
         * It helps to make sure all variables changed are notified
         * @param $scope {object}
         * @param fn {function}
         */
        this.safeApply = function($scope, fn) {
            var phase = $scope.$root.$$phase;
            if(phase === '$apply' || phase === '$digest') {
                if(fn && (typeof(fn) === 'function')) {
                    fn();
                }
            } else {
                $scope.$apply(fn);
            }
        };


        /**
         * It helps set all the parameters in an object to null. You can indicate properties to exclude
         * https://stackoverflow.com/questions/1181575/determine-whether-an-array-contains-a-value
         * @param {object} $scope
         * @param {object} $obj
         * @param {array} $exclude
         * @returns {object|null}
         */
        this.nullifyObject = function($scope, $obj, $exclude) {
            $exclude= $exclude===undefined || $exclude === null ?[]:$exclude;

            if($obj===undefined||$obj===null || typeof $obj !== 'object' || typeof $exclude !== 'object') return null;
            this.safeApply($scope, function () {
                // nullify all parameters
                Object.keys($obj).map(function (key) {
                    if($exclude.indexOf(key)<0)
                        $obj[key]=null;
                });
            });
            return $obj;
        };



        /**
         * It helps set all the parameters in an object to undefine. You can indicate properties to exclude
         * @param {object} $scope
         * @param {object} $obj
         * @param {array} $exclude
         * @returns {object|null}
         */
        this.unassignifyObject = function($scope, $obj, $exclude) {
            $exclude= $exclude===undefined || $exclude === null ?[]:$exclude;

            if($obj===undefined||$obj===null || typeof $obj !== 'object' || typeof $exclude !== 'object') return null;
            this.safeApply($scope, function () {
                // nullify all parameters
                Object.keys($obj).map(function (key) {
                    if($exclude.indexOf(key)<0)
                        delete $obj[key];
                });
            });
            return $obj;
        };





        /**
         * It helps to fetch a single object from multiple array of objects using a specific
         * property of each object to compare and picking the first one or returns null
         * Usage: $spicy.fetchObjectWithProperty($scope.array_of_objects,'id',  valueOfId)
         * @param arrayOfObjects array
         * @param propertyName string
         * @param propertyValue mixed
         * @returns {null}|{object}
         */
        this.fetchObjectWithProperty = function(arrayOfObjects, propertyName, propertyValue) {
            if(arrayOfObjects===null||arrayOfObjects=== undefined || !Array.isArray(arrayOfObjects))
                return null;

            $objectArray = arrayOfObjects.filter(function ($object) {
                if($object[propertyName]===propertyValue)
                    return true;
            });

            if($objectArray.length>=0) return $objectArray[0];
            return null;
        }
    });
}



