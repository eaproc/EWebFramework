

$(document).ready(function () {



    /*=========================================================================================
        Description: Append Client TimeZone to XHR Requests as header
        ----------------------------------------------------------------------------------------
    ==========================================================================================*/


    // https://stackoverflow.com/questions/1091372/getting-the-clients-timezone-in-javascript
    var offsetMins =   -(new Date().getTimezoneOffset()) ;
    var offset =  offsetMins/60;
    var hr = Math.floor(offset);
    var m = (offset%hr)*60;
    var offsetParsed = (offset>0? "+":"-") + hr.toString().padStart(2,"0") + ":" + m.toString().padStart(2,"0") ;

    xhook.before(function(request) {
        request.headers["client-time-zone"] = offsetParsed;
    });

    //
    // xhook.after(function(request, response) {
    //     console.log(request);
    // });


});