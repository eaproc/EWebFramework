using EWebFramework.Vendor.api.middlewares;
using EWebFramework.Vendor.api.utils;
using static EWebFramework.Vendor.PageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.api.middlewares
{
    public class InternalServerExceptionTracer : IMiddlewareCheckConstraint
    {
        /**
         * --------------------------------------------------------------------------
         *  USEFUL TIPS: If you don't want to loose exception trace,
         *  CATCH it at the method of occurrence, then rethrow it AS *innerException*
         * -------------------------------------------------------------------------
         * 
         **/
        private Exception exception;
        public readonly String TraceID;
        public InternalServerExceptionTracer(Exception ex)
        {
            this.exception = ex;
            this.TraceID = AlphaNumericCodeGenerator.RandomString(10);

        }


        /// <summary>
        /// No exception is thrown
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            return this.Check(request: HttpContext.Current.Request, response: HttpContext.Current.Response, session: HttpContext.Current.Session);
        }


        public bool Check(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            if (!SilentCheck(request, response, session))
            {
               Logger.Print("Application could not log Exception to Database!");
                return false;
            }

            return true;

        }


        public bool SilentCheck(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            

            try
            {
                SessionHelper sh = new SessionHelper();
                RequestHelper requestHelper = new RequestHelper(request);

               // return T___ServerExceptionLogger.add(pComments: null,
               //    pExceptionMessage: this.exception.Message, pCreatedAt: DateTime.Now,
               //    pIsResolved: false, pStackTrace: Logger.Read( this.exception ),
               //    pTraceID: this.TraceID, pRequestParametersJSON: requestHelper.ToJson(),
               //    pAbsoluteURL: request.RawUrl, pIPAddress: sh.ipAdress, pUserID: sh.userId 
               //);

            }
            catch (Exception e)
            {
                Logger.Print(e);
            }

            return false;
        }


    }
}