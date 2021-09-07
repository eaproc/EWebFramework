using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using EWebFramework.Vendor.api.middlewares;
using EWebFramework.Vendor.api.utils;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.api.middlewares
{
    /// <summary>
    /// Try to fix session logging out indefinitely. To Improve debbuging
    /// </summary>
    public class AutoFixNonExpiredSession : IMiddlewareCheckConstraint
    {

        public bool Check(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            var currentSessionHelper = new SessionHelper(session, request);
            var service = new api.services.SessionService(currentSessionHelper);

            if (currentSessionHelper.userId==null)
            {

                //T___Session pInSession = service.FetchNonExpiredValidSession(currentSessionHelper);
                //if (pInSession!=null )
                //{
                //    // restore session
                //    //var storedSession = utils.JsonDeserializer.deserializeToObjectType<utils.SessionHelper>(pInSession.session_variables);
                //    var deserialized = utils.JsonDeserializer.deserializeToDictionary(pInSession.SessionVariables);
                //    if(deserialized==null)
                //    {
                //        Program.Logger.Print("pInSession was NOT null but could not deserialize variables: " + pInSession.SessionVariables);
                //        return true; 
                //    }

                //    var storedSession = new utils.SessionHelper (deserialized );
                //    utils.SessionHelper.RestoreSessionVariables(storedSession, session);
                //}
            }


            return true;

        }

        public bool SilentCheck(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            try
            {
                return Check(request, response,session);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return false;
            }
        }
    }
}