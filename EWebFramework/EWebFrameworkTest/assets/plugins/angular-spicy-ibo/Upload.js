(function () {
    angular.module('Upload', [])
        .directive('fileModel', ['$parse','$log', function ($parse,$log) {
            return {
                restrict: 'A',
                link: function (scope, element, attrs) {

                    var model = $parse(attrs.fileModel);
                    var modelSetter = model.assign;

                    /**
                     * ID is required to be set on this element to work
                     */
                    if(!attrs.id) throw "Please, set an ID attribute on this model element " + attrs.fileModel + "\n";


                    element.bind('change', function () {
                        scope.$apply(function () {

                            var fileUploaded = element[0].files[0];


                            var operations = {
                                /**
                                 * It will reset the element. NOTE: element must have ID
                                 * @param elementId
                                 */
                              resetInputElement: resetInputElement,
                              createFileDimensionsOnImage: function (fnCallback) {
                                  // if the type isn't image or couldn't load it won't validate
                                  this.onError = function(){
                                      fileUploaded.dimensions = {
                                          width: 0,
                                          height: 0
                                      };
                                      fnCallback();
                                  };

                                  window.URL = window.URL || window.webkitURL;
                                  var img = new Image();
                                  img.src = window.URL.createObjectURL( fileUploaded );
                                  img.onerror = this.onError;
                                  img.onload = function() {
                                      fileUploaded.dimensions = {
                                          width: img.naturalWidth,
                                          height: img.naturalHeight
                                      };
                                      fnCallback();
                                  };
                              },
                                /**
                                 * Makes sure file name doesn't exceed a specific length
                                 * @return {boolean}
                                 */
                                isPassedNameLength: function () {
                                    var allowNameLength = $(element).attr("data-allow-name-length");
                                    if(!allowNameLength) allowNameLength = 50;

                                    if (fileUploaded.name.length > allowNameLength )
                                    {
                                        var msg =  'undefined' !== typeof attrs.warningNameLength ? attrs.warningNameLength :
                                            "File name is too long (" + fileUploaded.name.length.toString()
                                            + " characters)! \nMaximum File name length allowed is " + allowNameLength + " characters.";
                                        displayErrorMessage(msg);
                                        return false;
                                    }

                                    return true; // passed because it was not set
                                },
                              isPassedFileSizeRestriction: function () {
                                  var allowKbSize = $(element).attr("data-allow-kb-size");
                                  if (
                                      'undefined' !== typeof allowKbSize
                                  ) {
                                      if(!$.isNumeric(allowKbSize)) throw "Only numbers are allowed for this parameter!";

                                      var maxLength = Number(allowKbSize);
                                      if (fileUploaded.size > (maxLength*1000) )
                                      {
                                          var msg =  'undefined' !== typeof attrs.warningFileSize ? attrs.warningFileSize : "Maximum File Size allowed is " + maxLength + " KB!";
                                          displayErrorMessage(msg);
                                          return false;
                                      }
                                  }
                                  return true; // passed because it was not set
                              },
                                isPassedFileTypeRestriction: function () {
                                    if (
                                        'undefined' !== typeof attrs.allowTypes
                                    ) {
                                        var allowedTypes = attrs.allowTypes.split(";");
                                        if (!allowedTypes.includes(fileUploaded.type))
                                        {
                                            var default_warning = fileUploaded.type + "- file type is not allowed!";
                                            var msg =  'undefined' !== typeof attrs.warningFileTypes ? attrs.warningFileTypes : default_warning;
                                            displayErrorMessage(msg);
                                            console.log(default_warning); // for developer
                                            return false;
                                        }
                                    }
                                    return true; // passed because it was not set
                                },
                                /**
                                 * You must use File Type Restriction because this
                                 * function is meant for images ONLY!
                                 * @return {boolean}
                                 */
                                isPassedFileDimensionRestriction: function () {
                                    var allowDimensionWidth = $(element).attr("data-allow-dimension-width");
                                    var allowDimensionHeight = $(element).attr("data-allow-dimension-height");
                                    var warningDimensionWidth = $(element).attr("data-warning-dimension-width");
                                    var warningDimensionHeight = $(element).attr("data-warning-dimension-height");

                                    if('undefined' !== typeof allowDimensionWidth || 'undefined' !== typeof allowDimensionHeight)
                                    {
                                        // insanity check :)
                                        if('undefined' === typeof attrs.allowTypes) throw "Please, add file type restriction to restrict this to images ONLY!";
                                    }else
                                        return true; // you shouldn't be here




                                    // (dimension,rule{exact,minimum,maximum}
                                    if('undefined' !== typeof allowDimensionWidth)
                                    {
                                        var allowDimensionWidthValues = allowDimensionWidth.split(";");
                                        var width = Number(allowDimensionWidthValues[0]);
                                        var rule  = allowDimensionWidthValues.length > 1 ? allowDimensionWidthValues[1] : "minimum";

                                        var passed = false;
                                        switch (rule)
                                        {
                                            case "minimum":
                                                passed = fileUploaded.dimensions.width >= width;
                                                break;
                                            case "maximum":
                                                passed = fileUploaded.dimensions.width <= width;
                                                break;
                                            case "exact":
                                                passed = fileUploaded.dimensions.width === width;
                                                break;
                                        }


                                        var msg =  'undefined' !== typeof warningDimensionWidth ? warningDimensionWidth : "Wrong Width Dimension for Image!";
                                        if(!passed)
                                        {
                                            displayErrorMessage(msg);
                                            return false; // don't proceed since this failed
                                        }

                                    }



                                    if('undefined' !== typeof allowDimensionHeight)
                                    {
                                        var allowDimensionHeightValues = allowDimensionHeight.split(";");
                                        var height = Number(allowDimensionHeightValues[0]);
                                        rule  = allowDimensionHeightValues.length > 1 ? allowDimensionHeightValues[1] : "minimum";

                                        passed = false;
                                        switch (rule)
                                        {
                                            case "minimum":
                                                passed = fileUploaded.dimensions.height >= height;
                                                break;
                                            case "maximum":
                                                passed = fileUploaded.dimensions.height <= height;
                                                break;
                                            case "exact":
                                                passed = fileUploaded.dimensions.height === height;
                                                break;
                                        }


                                        msg =  'undefined' !== typeof warningDimensionHeight ? warningDimensionHeight : "Wrong Height Dimension for Image!";
                                        if(!passed)
                                        {
                                            displayErrorMessage(msg);
                                            return false; // don't proceed since this failed
                                        }

                                    }

                                        return true ; // passed because it was not set
                                },
                                processSetting: function () {

                                    /**
                                     * If failed any validation reset element then return
                                     * This validation are synchronous and must be passed before anything else happens
                                     */
                                    if (
                                        !operations.isPassedNameLength()
                                        || !operations.isPassedFileSizeRestriction()
                                        || !operations.isPassedFileTypeRestriction()
                                        || !operations.isPassedFileDimensionRestriction()
                                    ) {
                                        operations.resetInputElement(attrs.id);
                                        //
                                        // Nullify the File on  the Model
                                        modelSetter(scope, null);

                                        // clear rendered image as well if specified
                                        if (attrs.render !== undefined) $("#" + attrs.render).attr('src', null);

                                        return;
                                    } else
                                    {

                                        //
                                        // Render if required
                                        //
                                        if(attrs.render !== undefined)
                                        {
                                            var reader = new FileReader();
                                            reader.onload = function (e) {

                                                $("#"+attrs.render).attr('src', e.target.result);
                                                //
                                                // if other operations are required after successful render
                                                //                                         onSuccessCreateCropper
                                                // console.log($("#"+attrs.render).attr('src'));   //base64 URL
                                                if(attrs.onSuccessRender)
                                                    eval(attrs.onSuccessRender)(fileUploaded);


                                            };
                                            reader.readAsDataURL(fileUploaded);
                                        }


                                        //
                                        // Finally Set the File on  the Model
                                        modelSetter(scope,fileUploaded);
                                        //
                                        // If you need information that the model has been set
                                        if(attrs.onSuccess)
                                            eval(attrs.onSuccess)(fileUploaded);

                                    }


                                }
                            };
                            
                            
                            /**
                             * If no file uploaded then return
                             */
                            if ( element[0].files.length === 0 ) return;


                            //**
                            //**
                            // attrs is not using updated attribute data
                            // it only uses attribute on when the element was created
                            // So don't use it on dynamic attributes for fetching
                            if(attrs.render !== undefined)
                            {
                                // if there is other dependencies, just invoke them before doing real processing
                                return operations.createFileDimensionsOnImage(
                                    operations.processSetting
                                );
                            }
                            else
                            {
                                return  operations.processSetting();
                            }





                            /**
                             |
                             | It won't allow the file to be read if it is large
                             |              like (data-allow-kb-size=10000)    - if not set, any size is allowed
                             |
                             | It won't allow file read if there is filter for file types separated with semi-colon
                             |              like (data-allow-types="image/jpeg;image/png")  - if not set, any file type is allowed
                             |
                             | It allows image of specific dimensions (dimension,rule{exact,minimum,maximum}
                             |              like (data-allow-dimension-width="300;minimum:default-minimum")  - if not set, any dimension width is allowed
                             |
                             | It allows image of specific dimensions (dimension,rule{exact,minimum,maximum}
                             |              like (data-allow-dimension-height="300;minimum:default-minimum")  - if not set, any dimension height is allowed
                             |
                             |
                             |
                             |
                             | DATA-WARNINGS
                             |     - works with this attribute to display custom message for file types warning
                             |              like (data-warning-file-types="Please, upload JPEG Image File only!") - if not set: "That type of file is not allowed!"
                             |
                             |     - works with this attribute to display custom message for large file size warning
                             |              like (data-warning-file-size="Please, upload maximum of 500KB!") - if not set: ""Maximum File Size allowed is " + maxLength + " KB!"
                             |
                             |     - Display custom message for wrong width dimension
                             |              like (data-warning-dimension-width="Image must have minimum width dimension of 300pixels!") - if not set: "Wrong Width Dimension for Image!"
                             |
                             |     - Display custom message for wrong height dimension
                             |              like (data-warning-dimension-height="Image must have minimum height dimension of 300pixels!") - if not set: "Wrong Height Dimension for Image!"
                             |
                             /*************************************/






                        });
                    });
                }
            };
        }])
        .service('Upload', ['$http', function ($http) {

            this.uploadAdvance = function (PostURL, ObjectFields, ArrayObjectFiles, onComplete, onSuccess) {
                var formData = new FormData();

                /**
                 *  Add Fields first
                 */
                for (var j = 0 in ObjectFields) {
                    var b = ObjectFields[j];
                    if(b && b instanceof Blob )
                        formData.append(j, b, b.name);
                    else
                        formData.append(j, b);
                }


                /**
                 *  Add Files if any is available
                 */
                for(var i = 0 in ArrayObjectFiles)
                {
                    var lFile = ArrayObjectFiles[i];
                    var lFileName = Object.keys(lFile)[0];
                    var lFileValue = lFile[lFileName];

                    var b = lFileValue;
                    if(b && b instanceof Blob )
                        formData.append(lFileName, b, b.name);
                    else
                        formData.append(lFileName, b);

                }


                /**
                 *  Post Data
                 */
                $http({
                    method: 'POST',
                    url: PostURL,
                    transformRequest: angular.identity,
                    headers: {'Content-Type': undefined},
                    data: formData
                }).then(function successCallback(response) {
                    if(onComplete!==undefined) onComplete(response.data);
                    if(onSuccess!==undefined && response.status === 200) onSuccess(response.data);

                }, function errorCallback(response) {

                    if(onComplete!==undefined) onComplete(response.data);

                    console.log(response); // internal error occurred
                });


            };

        }])
        .service('Cropper',function () {

            //https://foliotek.github.io/Croppie/

            /**
             * This assumes a cropper works once per image!
             * You can't crop multiple files at the same time
             * @param angularScope
             * @param onCompleteFunction
             * @constructor
             */

            this.$scope = null;
            this.$spicy = null;
            this._isInitialized = false;
            var self = this;

            this.initialize = function ($scope,$spicy) {
                if(self._isInitialized) throw "You can only initialize this service once!";

                $scope.cropperTools = {
                    CROPPER_CONTROL: null,
                    CROPPER_CONTROL_ORIGINAL_HTML: $("#Cropper_ImageContainer").html(),
                    Cropper_ImageElementControlData: null,
                    onPickerSuccessCreateCropper : this.onPickerSuccessCreateCropper
                };
                this.$scope = $scope;
                this.$spicy = $spicy;

                this.resetCropper();


                $('#Cropper_Modal').on('shown.bs.modal', function () {

                    // call select file immediately on shown
                    triggerFilePickerButton('Cropper_Hidden_Picture_Picker');

                });

                self._isInitialized = true;

            };


            /**
             * On result is called with a value if picked else it will be undefined
             * @param {int} MinSizeNeededWidth
             * @param {int} MinSizeNeededHeight
             * @param {boolean} isRectangle
             * @param {function} onResult
             * @constructor
             */
            this.Crop = function (MinSizeNeededWidth,MinSizeNeededHeight,MaximumKbSizeAllowed, isRectangle, onResult) {
                if(!this.$scope || ! this.$spicy) throw "Please, invoke Cropper.initialiaze($scope) first!";


                this.$scope.cropperTools.dimensions = {width: MinSizeNeededHeight, height: MinSizeNeededHeight};
                this.$scope.cropperTools.onResult = onResult;
                this.$scope.cropperTools.onAcceptResult = this.onAcceptResult;
                this.$scope.cropperTools.onCancelResult = this.onCancelResult;
                this.$scope.cropperTools.isRectangularImage = isRectangle;

                // set validation rules
                Cropper_Hidden_Picture_Picker = $("#Cropper_Hidden_Picture_Picker");
                Cropper_Hidden_Picture_Picker.attr("data-warning-dimension-height","Minimum of "+ MinSizeNeededHeight.toString() +" pixels height image is allowed!");
                Cropper_Hidden_Picture_Picker.attr("data-allow-dimension-height",MinSizeNeededHeight.toString() + ";minimum");
                Cropper_Hidden_Picture_Picker.attr("data-warning-dimension-width","Minimum of "+ MinSizeNeededWidth.toString() +" pixels width image is allowed!");
                Cropper_Hidden_Picture_Picker.attr("data-allow-dimension-width",MinSizeNeededWidth.toString() + ";minimum");
                Cropper_Hidden_Picture_Picker.attr("data-allow-kb-size",MaximumKbSizeAllowed.toString());


                /**
                 *  Reset cropper data picker control so new file picked event can be called on same file
                 */
                resetInputElement("Cropper_Hidden_Picture_Picker");



                // show modal for picking
                $('#Cropper_Modal').modal({
                    keyboard: false,
                    backdrop: 'static'
                });


            };


            /**
             * This is called when the picker validates and successfully picks an image
             * @param {File} $imageFileRendered
             */
            this.onPickerSuccessCreateCropper = function($imageFileRendered){
                var MinSizeNeededWidth = self.$scope.cropperTools.dimensions.width;
                var MinSizeNeededHeight = self.$scope.cropperTools.dimensions.height;
                var cropBoxElement = $("#Cropper_ImageElementControl");
                $this = self;

                if($this.$scope.cropperTools.CROPPER_CONTROL)
                {
                    $this.resetCropper();
                    cropBoxElement = $("#Cropper_ImageElementControl");
                }


                //file as been accepted at this point
                $this.$spicy.safeApply($this.$scope, function () {
                    // clone it
                    $this.$scope.cropperTools.Cropper_ImageElementControlData = $.extend({}, $imageFileRendered);

                });



                cropBoxElement.attr( "src", window.URL.createObjectURL( $imageFileRendered ));
                cropBoxElement.on( "load", function() {
                    // // initiate cropper
                    // initiate cropper
                    cropBoxElement.removeClass("hidden"); //make element visible now that there is image
                    $this.$scope.cropperTools.CROPPER_CONTROL =  cropBoxElement.croppie(
                        {
                            viewport:{
                                width: MinSizeNeededWidth, height: MinSizeNeededHeight,
                                type: $this.$scope.cropperTools.isRectangularImage? 'square' : 'circle'
                            },
                            showZoomer:false
                        }
                    );

                });


            };


            this.resetCropper = function(){
                if(this.$scope.cropperTools.CROPPER_CONTROL) this.$scope.cropperTools.CROPPER_CONTROL.croppie('destroy');
                $("#Cropper_ImageContainer").html(this.$scope.cropperTools.CROPPER_CONTROL_ORIGINAL_HTML);
                $("#Cropper_ImageElementControl").addClass("hidden"); // hide the image display since no image
                $this = this;
                this.$spicy.safeApply(this.$scope, function () {
                    $this.$scope.cropperTools.Cropper_ImageElementControlData = null; // clear this incase of listeners
                });

            };


            this.onAcceptResult = function()
            {
                $this = self;
                $this.$scope.cropperTools.CROPPER_CONTROL.croppie('result',{
                    type: 'blob',
                    size: 'viewport',
                    format: 'png',
                    quality: 1,
                    circle: $this.$scope.cropperTools.isRectangularImage? false : true //false
                }).then(function(result){


                    //close modal
                    $('#Cropper_Modal').modal('hide');

                    $this.$scope.cropperTools.onResult(FileManipulators.blobToFile(result, $this.$scope.cropperTools.Cropper_ImageElementControlData.name ));

                    // Now reset
                    $this.resetCropper();



                })

            };


            this.onCancelResult = function () {
                self.resetCropper();
                //close modal
                $('#Cropper_Modal').modal('hide');
                self.$scope.cropperTools.onResult();
            };


        })
    ;


})();



//
// Sample
//
//

// <input type="file"
// file-model="v___editor.File__PROFILE_PICTURE"
// data-allow-kb-size="1000"
// data-warning-file-size="Please, upload maximum of 100KB image!"
// data-allow-types="image/jpeg;image/png"
// data-warning-file-types="Please, upload ONLY JPEG and PNG images!"
// data-allow-dimension-width="400;exact"
// data-warning-dimension-width="Only 400 pixels width image is allowed!"
// data-allow-dimension-height="400;exact"
// data-warning-dimension-height="Only 400 pixels height image is allowed!"
// data-render="imgProfilePicture"
// data-allow-name-length="50"
// data-warning-name-length="Please, rename the file to a short name with maximum 30 characters!"
//data-on-success-render="appScope.onSuccessCreateCropper"
//data-on-success="appScope.onSuccess"
// id="File__PROFILE_PICTURE" />


