using EWebFramework.Vendor.scheduled_jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EWebFramework.Vendor.scheduled_jobs.BaseJob;

namespace EWebFramework.scheduled_jobs
{


    // INSERT THIS TO APPLICATION_START iN GLOBAL ASAX
    // ---------------------------------------------------


    //new app.scheduled_jobs.CronJob(
    //      new app.scheduled_jobs.SampleJob() { DebugMode = true },
    //      new app.scheduled_jobs.SampleJob() { DebugMode = true }
    //  );

    // ---------------------------------------------------





    /// <summary>
    /// </summary>
    public class SampleJob : BaseJob
    {
        public SampleJob():base(pExecuteOnTime: new TimeSpan(6, 52, 00), pJobMode: JobInterval.FOUR_HOURLY,  pExecuteOnDay: DateTime.Now )
        {
            // TODO: Add constructor logic here
            //
            //@@CREATE_LOGGERS


        }



        public override string JobName()
        {
            return "SampleJob";
        }

        protected override void onJobCalled()
        {
        }

        protected override void onJobCreated()
        {

        }

        protected override void onJobTerminated()
        {

        }
    }

}
