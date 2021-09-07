using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EWebFramework.response_handlers;
using EWebFramework.api.services;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EWebFramework.Vendor.response_handlers;

namespace EWebFramework.controllers.errors
{
    public class Error500
    {



        public static void Index(api.middlewares.InternalServerExceptionTracer internalServerExceptionTracer)
        {


            BasicResponse.OutputPage("views/errors/Error500.html",
                pInjectData: new KeyValuePair<string, object>("$$trace_id",
                                      internalServerExceptionTracer.TraceID
                                 ), ResponseCode: 200
                );


        }


    }
}