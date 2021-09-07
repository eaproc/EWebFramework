function toUrl($path) {
    return window.location.origin + "/?p=" + $path;
}

function toUrlApi($path, $objParameters) {
    var str = $objParameters? "&" + jQuery.param( $objParameters ) : "";
    return window.location.origin + "/api/?p=" + $path + str;
}



function getURLParameter (sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
}


function downloadFileURL(sURL) {
    location.href = sURL;
}