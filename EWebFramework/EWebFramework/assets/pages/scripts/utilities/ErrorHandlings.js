

function detectAndThrow500Error(response) {
    if(response.status === 500)
    {
        var ResponseJSON = typeof response.data === 'string'? JSON.parse(response.data) : response.data;
        var TraceID = ResponseJSON.data? ResponseJSON.data.TraceID: '';
        displayErrorMessage("Server encountered a problem with your request. " +
            "\nApologies, our engineers have been notified of this issue! " +
            "\n\nYou can follow up with this TraceID: " + TraceID + "\n");
    }
}


function detectAndThrow503Error(response) {
    if(response.status === 503) //Maintenance Mode
    {
        displayErrorMessage("Application is in maintenance mode!", function () {
            location.href="/";
        });
    }
}




function detectAndThrow401Error(response) {
    if(response.status === 401)
    {
        var ResponseJSON = typeof response.data === 'string'? JSON.parse(response.data) : response.data;
        var msg = ResponseJSON!==null && ResponseJSON!==undefined ? ResponseJSON.message: '';
        var l = response.finalUrl? response.finalUrl : '';
        callBack = msg.indexOf("You must be logged in") >=0 ? function(){window.location.reload();} : null;
        displayErrorMessage(msg + "\n" + l, callBack);
    }
}

function detectAndThrow417Error(response) {
    if(response.status === 417)
    {
        var ResponseJSON = typeof response.data === 'string'? JSON.parse(response.data) : response.data;
        console.log(ResponseJSON);
        var msg = ResponseJSON.message;
        displayErrorMessage(msg);
    }
}






$(document).ready(function () {



    /*=========================================================================================
        Description: Handle other Errors Except 200 Response Status Code
        ----------------------------------------------------------------------------------------
    ==========================================================================================*/

    xhook.before(function(request) {
        // Before it is sent

    });


    xhook.after(function(request, response) {
        // on received
        // dd(request, response);

        detectAndThrow500Error(response);
        detectAndThrow401Error(response);
        detectAndThrow417Error(response);

    });


});