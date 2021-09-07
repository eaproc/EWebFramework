
using DB.Abstracts;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPRO.Library.Objects;
using EPRO.Library.Types;
using EWebFramework.Vendor.api.utils;

namespace EWebFramework.api.services.Auth
{
    public class Logout:ClientService
    {

        public Logout(SessionHelper sessionHelper) : base(sessionHelper)
        {

        }




//        public void CheckoutMyPendingPage()
//        {

//            //Complete Waiting Page
//            //
//            //
//            var d = DatabaseInit.DBConnectInterface.getDBConn();

//            String SQL =
//"                  " + Environment.NewLine +
//"update auth.UserVisitedPage                  " + Environment.NewLine +
//"set auth.UserVisitedPage.CheckedOutTime={0}                  " + Environment.NewLine +
//"where auth.UserVisitedPage.UserID={1} and auth.UserVisitedPage.SessionID='{2}' and TimedOut=0                  " + Environment.NewLine;

//            SQL = String.Format(
//                    SQL,
//                    d.GetSQLDateTimeFormat(new NullableDateTime(ClientService.ServerNowDateTime)),
//                    this.sessionHelper.userId.Value,
//                    this.sessionHelper.sessionId
//                );

//            this.ExecuteQuery(SQL);

//        }




    }
}