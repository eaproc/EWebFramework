using EWebFramework.Vendor.api.utils.JsonReplies;
using EWebFramework.Vendor.exceptions;
using EWebFramework.Vendor.exceptions.auth;
using EWebFramework.Vendor.response_handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




            try
            {


                HandleWebPageRequest(routingList: new EWebFramework.routes.WEB(),
                   preScript: () => {

                       ///**
                       //   * --------------------------------------
                       //   *  INITIALIZE TIME ZONE FOR DATABASE
                       //   * --------------------------------------
                       //   * 
                       //   * **/
                       //api.services.DatabaseTimeZoneUtils.InitializeDatabaseTimeZoneUtils(Request);


                       //
                       //  Persist Session
                       //  If same session id exist and has not expired on database and user_id is not null
                       //  and this current request has expired
                       //  restore it
                       //  
                       //  on logout, delete session from database
                       //


                       new EWebFramework.api.middlewares.AutoFixNonExpiredSession().Check(request: Request, response: Response, session: Session);


                   },
                   postScript: () => {

                       var InsertedSessionIntoDatabase = new EWebFramework.api.middlewares.InsertOrUpdateSessionRequest().Check(request: Request, response: Response, session: Session);

                   });


            }
            catch (GuestURLAccessOnlyException)
            {
                Response.Redirect("?p=/home", true);
            }
            catch (System.Threading.ThreadAbortException) { }
            catch (ExpectationFailedException ex)
            {
                //I assumed you are logged in and have a role selected
                EWebFramework.controllers.errors.Error417.Index(Request, Response, Session, ex.Message);
            }
            catch (VarDumpException ex)
            {
                this.OutputJSON(new EWebFramework.Vendor.api.utils.JsonReplies.FailedResult(pMessage: ex.Message, pData: ex.dumpedData));
            }
            catch (Exception ex)
            {
                //// Don't use this if you didn't create the database for it
                //var p = new api.middlewares.InternalServerExceptionTracer(ex);
                //if (p.Check(request: Request, response: Response, session: Session))
                //    controllers.errors.Error500.Index(p);
                //else
                //{
                //    Logger.Print(ex);
                //    this.OutputJSON(new FailedResult(pMessage: ex.Message));
                //}



                Logger.Print(ex);
                this.OutputJSON(new FailedResult(pMessage: ex.Message));


            }

        }
    }
}