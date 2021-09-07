using EWebFramework.api.middlewares;
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

namespace EWebFramework.api
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


           try
            {



                HandleAPIPageRequest(routingList: new EWebFramework.routes.API(),
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


                        new AutoFixNonExpiredSession().Check(request: Request, response: Response, session: Session);


                    },
                    postScript: () => {

                        var InsertedSessionIntoDatabase = new InsertOrUpdateSessionRequest().Check(request: Request, response: Response, session: Session);

                    });


            }
            catch (GuestURLAccessOnlyException ex)
            {
                this.OutputJSON(new FailedResult(pMessage: ex.Message), 403);
            }
            catch (UnAuthorizedURLAccess ex)
            {
                this.OutputJSON(new FailedResult(pMessage: ex.Message), 401);
            }
            catch (URLAccessUnPermitted ex)
            {
                this.OutputJSON(new FailedResult(pMessage: ex.Message), 401);
            }
            catch (ExpectationFailedException ex)
            {
                this.OutputJSON(new FailedResult(pMessage: ex.Message), 417);
            }
            catch (VarDumpException ex)
            {
                this.OutputJSON(new FailedResult(pMessage: ex.Message, pData: ex.dumpedData));
            }
            catch (InvalidURLException ex)
            {
                this.OutputJSON(new FailedResult(pMessage: ex.Message));
            }
            catch (Exception ex)
            {
                //// Don't use this if you didn't create the database for it
                //var p = new api.middlewares.InternalServerExceptionTracer(ex);
                //if (p.Check(request: Request, response: Response, session: Session))
                //    this.OutputJSON(
                //        new FailedResult(pMessage: "Internal Server Error", pData: new
                //        {
                //            TraceID = p.TraceID
                //        }
                //        ), 500);
                //else
                //{
                //    // This is crazy error I need to check manually
                //    this.OutputJSON(
                //       new FailedResult(pMessage: "Internal Server Error", pData: new
                //       {
                //           TraceID = "SCAD-DB-001" //SQL ERROR
                //       }
                //       ), 500);

                //}


                this.OutputJSON(
                    new FailedResult(pMessage: "Internal Server Error", pData: new
                    {
                        TraceID = "SCAD-DB-001", //SQL ERROR
                        RawDetail = ex.Message
                    }
                    ), 500);


            }



        }
    }
}