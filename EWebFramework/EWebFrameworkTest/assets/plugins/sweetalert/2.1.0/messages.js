/**
 * Created by ibo on 9/28/17.
 */
/*
|--------------------------------------------------------------------------
| Messages
|--------------------------------------------------------------------------
| Requires Sweet alert plugin
| (assets/global/plugins/sweetalert2/6.9.1/sweetalert2.min.js)
|
| https://sweetalert.js.org/docs/
*/


function displaySuccessMessage(pMessage, pCallBackOnSuccess)
{
    if(pMessage!==null && pMessage!==undefined)
        pMessage = pMessage.replace("<br/>","\n");

    swal({
        title: "SUCCESS!",
        text: pMessage,
        icon: "success",
        buttons: {
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: "btn-success",
                closeModal: true
            }
        }
    }).then(
        function () {
            if (pCallBackOnSuccess !== undefined && pCallBackOnSuccess !== null) pCallBackOnSuccess();
        }
    );
}




function displayErrorMessage(pMessage, pCallBackOnSuccess) {

    if(pMessage!==null && pMessage!==undefined)
        pMessage = pMessage.replace(/<br\/>/g,"\n");


    swal({
        title: "ERROR!",
        text: pMessage,
        icon: "error",
        buttons: {
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: "btn-warning",
                closeModal: true
            }
        }
    }).then(
        function () {
            if (pCallBackOnSuccess !== undefined && pCallBackOnSuccess !== null) pCallBackOnSuccess();
        });
}





function confirmMessage(pMessage, pCallBackOnSuccess, pSuccessContext, pCallBackOnCancel, pCancelContext ) {

    if(pMessage!==null && pMessage!==undefined)
        pMessage = pMessage.replace("<br/>","\n");

    swal({
        title: "QUESTION?",
        text: pMessage,
        icon: 'warning',
        dangerMode: true,
        buttons: {
            cancel: {
                text: "NO",
                value: null,
                visible: true,
                className: "btn-default",
                closeModal: true,
            },
            confirm: {
                text: "YES",
                value: true,
                visible: true,
                className: "btn-danger",
                closeModal: true
            }
        }
    }).then(
        function (isConfirm) {

            if(isConfirm===true)
            {
                if (pCallBackOnSuccess !== undefined && pCallBackOnSuccess !== null) pCallBackOnSuccess(pSuccessContext);
            }else{
                    if (pCallBackOnCancel !== undefined && pCallBackOnCancel !== null) pCallBackOnCancel(pCancelContext);
                }

        }
    );


}



