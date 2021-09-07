using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.api.services
{
    public class SessionService:ClientService
    {
        public SessionService(SessionHelper sessionHelper) :base(sessionHelper)
        {

        }




        /// <summary>
        /// Inserts or replace session but throws exception
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public bool InsertOrReplace()
        {
            SessionHelper session = this.sessionHelper;

            //T___Session ps = T___SessionExtended.GetSessionWith(session.sessionId);
            //if (ps.isValidRow)
            //{

            //    //update
            //    T___Session.update(pID: ps.ID, pSessionID: session.sessionId, pSessionTimeoutMins: session.sessionTimeOutInMins,
            //                        pUserID: DataColumnParameter.TranslateNothingToNull(session.userId),
            //                        pIsNewSession: session.isNewSession, pIsReadOnly: session.isReadonly,
            //                        pUpdatedAt: ServerNowDateTime, pIpAddress: session.ipAdress, pBrowser: session.browser,
            //                        pSessionVariables: session.ToJson(), pLastActive: ServerNowDateTime
            //                        );
            //}
            //else
            //{
            //    //insert
            //    return T___Session.add(pSessionID: session.sessionId, pSessionTimeoutMins: session.sessionTimeOutInMins,
            //                         pUserID: DataColumnParameter.TranslateNothingToNull(session.userId),
            //                         pIsNewSession: session.isNewSession, pIsReadOnly: session.isReadonly,
            //                         pUpdatedAt: ServerNowDateTime, pIpAddress: session.ipAdress, pBrowser: session.browser,
            //                         pSessionVariables: session.ToJson(), pLastActive: ServerNowDateTime, pCreatedAt: ServerNowDateTime
            //                        );
            //}


            return true;
        }


        //public T___Session FetchNonExpiredValidSession(SessionHelper currentSessionHelper)
        //{
        //    var p = T___SessionExtended.GetSessionWith(currentSessionHelper.sessionId);
        //    if(p.isValidRow)
        //    {
        //        if(
        //            EPRO.Library.Objects.EDateTime.GetTimeDifferenceInMilliseconds(p.UpdatedAt.DateTimeValue,ServerNowDateTime) < (1000 * p.SessionTimeoutMins * 60)
        //            )
        //        {
        //            return p;
        //        }

        //    }

        //    return null;

        //}



        //public bool RemoveSession(String pSessionID)
        //{
        //    // Make Expired Instead of removing
        //    return T___SessionExtended.DeleteSessionWith(pSessionID);
        //}




    }
}