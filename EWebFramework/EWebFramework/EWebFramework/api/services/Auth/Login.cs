
using DB.Abstracts;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPRO.Library.Objects;
using EPRO.Library.Modules;
using EWebFramework.Vendor.api.utils;

namespace EWebFramework.api.services.Auth
{
    public class Login:ClientService
    {

        public Login(SessionHelper sessionHelper) : base(sessionHelper)
        {

        }


//        public T___Users AttemptLogin(String Username, String Password)
//        {
//            T___Users t___User = T___UsersExtended.GetUser(Username, Password);
//            if (!t___User.isValidRow) throw new Exception("Invalid Username or Password!");
//            if (!t___User.IsActive) throw new AccountDeactivatedException();
//            if (t___User.StudentID != 0) // STUDENT ACCOUNT
//            {
//                // Don't allow login if the student enrollment status is not active
//                T___Student student = T___Student.getRowWhereIDUsingSQL(t___User.StudentID);
//                T___StudentEnrollmentStatus studentEnrollmentStatus = T___StudentEnrollmentStatus.getRowWhereIDUsingSQL(student.StudentEnrollmentStatusID);
//                if (!studentEnrollmentStatus.CanAccess) throw new Exception("You can not access SCADWARE becuse your enrollment status is " + studentEnrollmentStatus.Definition);


//                // Allow complete login without checks
//                //T___StudentTermRegistration t___StudentTermRegistration = T___StudentTermRegistrationExtended.GetStudentActiveTermRegistration(t___User.StudentID);
//                //if (!t___StudentTermRegistration.isValidRow) throw new NoActiveTermRegistrationException();//No Active Term
//                //if(!t___StudentTermRegistration.HasScadwareAccess) throw new NoAccessSCADWAREException();
//                //if (t___StudentTermRegistration.LimitAccessWithDebt && true ) throw new NoAccessSCADWAREException(); //Check for debt
//            }


//            return t___User;
//        }

//        public void KeepLoginHistory()
//        {
//            // Renew
//            this.sessionHelper = new SessionHelper();

//            T___UserLoginHistory.add(pUserAgent: this.sessionHelper.browser, pCreatedAt: ServerNowDateTime,
//                pIPAddress: this.sessionHelper.ipAdress, pUserID: this.sessionHelper.userId.Value,
//                pSessionID: HttpContext.Current.Session.SessionID
//                );


//            // Keep Last 50 Logins
//            String SQL =
//"delete  from auth.UserLoginHistory                  " + Environment.NewLine +
//"where UserID ={0} and ID not in (                  " + Environment.NewLine +
//"                 select top 50 ID from auth.UserLoginHistory where UserID={0} order by ID desc                  " + Environment.NewLine +
//"    )                  " + Environment.NewLine +
//"                  ";

//            SQL = String.Format(SQL, this.sessionHelper.userId.Value);

//            ExecuteQuery(SQL);


//        }




//        public void TimeOutPendingWebView()
//        {

//            //Complete Waiting Page
//            //
//            //
//            var d = DatabaseInit.DBConnectInterface.getDBConn();

//            String SQL =
//"                  " + Environment.NewLine +
//"update auth.UserVisitedPage                  " + Environment.NewLine +
//"set auth.UserVisitedPage.TimedOut=1                   " + Environment.NewLine +
//"where auth.UserVisitedPage.UserID={0} and TimedOut=0 and auth.UserVisitedPage.CheckedOutTime IS NULL                 " + Environment.NewLine;

//            SQL = String.Format(
//                    SQL,
//                    this.sessionHelper.userId.Value
//                );

//            this.ExecuteQuery(SQL);

//        }



    }
}