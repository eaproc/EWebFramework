using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;

namespace EWebFramework.controllers.errors
{
    public class Error417
    {



        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session, String Message)
        {

            //BasicResponse.OutputMasterPageInherited("views/errors/Error417.html",
            //    pCompactData: api.controllers.Controller.CompactForInjection(
            //                      new KeyValuePair<string, object>("$$errorMessage",
            //                          Message
            //                     )
            //        ), ResponseCode: 200
            //    );



        }


    }
}