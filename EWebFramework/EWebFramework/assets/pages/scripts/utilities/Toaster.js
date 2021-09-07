/*
|--------------------------------------------------------------------------
| Toaster
|--------------------------------------------------------------------------
| Requires toastr plugin
| https://github.com/CodeSeven/toastr
|
| toastr.options.newestOnTop = false; DEFAULT Is true
| toastr.options.preventDuplicates = true; default is false, it will remove previous matching duplicate
|
| Supports HTML Messages
*/

// TOASTS
// ------------
// .success
// .info
// .warning
// .error
// ------------

// Force clear toastrs
// toastr.clear();

//"timeOut": 0,  /// stay forever or 3000 = 3secs
// "showMethod": "fadeIn",
// "hideMethod": "fadeOut", //slideUp or slideDown
// containerId: 'toast-top-center'  // if you need to reference the id
// or 'toast-top-center', //'toast-top-full-width'


function toastSuccessMessage(pMessage, pTitle)
{
    if(!pTitle) pTitle="SUCCESS";
    if(pMessage!==null && pMessage!==undefined)
        pMessage = pMessage.replace(/\n/g,"<br/>");

    toastr.success(
        pMessage,
        pTitle,
        {
            positionClass: 'toast-top-center',
            "closeButton": true,
            "progressBar": true
        }
    );

}




function toastErrorMessage(pMessage, pTitle) {

    if(!pTitle) pTitle="ERROR";
    if(pMessage!==null && pMessage!==undefined)
        pMessage = pMessage.replace(/\n/g,"<br/>");

    toastr.error(
        pMessage,
        pTitle,
        {
            positionClass: 'toast-top-center',
            "closeButton": true,
            "progressBar": true
        }
    );

}



function toastWarningMessage(pMessage, pTitle) {

    if(!pTitle) pTitle="WARNING";
    if(pMessage!==null && pMessage!==undefined)
        pMessage = pMessage.replace(/\n/g,"<br/>");

    toastr.warning(
        pMessage,
        pTitle,
        {
            positionClass: 'toast-top-center',
            "closeButton": true,
            "progressBar": true
        }
    );

}






function toastInfoMessage(pMessage, pTitle) {

    if(!pTitle) pTitle="INFO";
    if(pMessage!==null && pMessage!==undefined)
        pMessage = pMessage.replace(/\n/g,"<br/>");

    toastr.info(
        pMessage,
        pTitle,
        {
            positionClass: 'toast-top-center',
            "closeButton": true,
            "progressBar": true
        }
    );

}




