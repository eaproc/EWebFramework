
using EPRO.Library.Objects;
using EWebFramework.response_handlers;
using EWebFramework.Vendor.api.utils.JsonReplies;
using EWebFramework.Vendor.response_handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.console
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




            try
            {







                HandleConsolePageRequest(() => {
                    ///**
                    // * --------------------------------------
                    // *  INITIALIZE TIME ZONE FOR DATABASE
                    // * --------------------------------------
                    // * 
                    // * **/
                    //api.services.DatabaseTimeZoneUtils.InitializeDatabaseTimeZoneUtils(Request);

                }, () => { });








                //SessionHelper s = new SessionHelper(Session,Request);
                //this.OutputJSON(new SuccessResult(pData:s));
            }
            catch (VarDumpException ex)
            {
                this.OutputJSON(new FailedResult(pMessage: ex.Message, pData: ex.dumpedData));
            }
            catch (Exception ex)
            {
                Logger.Print(ex);
                this.OutputJSON(new FailedResult(pMessage: ex.Message));
            }





        }
    }
}