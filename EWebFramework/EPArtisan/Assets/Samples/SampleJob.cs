using EWebFramework.api.services;

namespace EWebFramework.scheduled_jobs
{


    // INSERT THIS TO APPLICATION_START iN GLOBAL ASAX
    // ---------------------------------------------------



    //EWebFramework.scheduled_jobs.SampleJob bSampleJob = new SampleJob();

    //Hangfire.RecurringJob.AddOrUpdate(
    //    recurringJobId: bSampleJob.GetType().FullName,
    //            methodCall: () => bSampleJob.HandleJob(),
    //            cronExpression: Hangfire.Cron.Daily(1,00)
    //        );

    // ---------------------------------------------------





    /// <summary>
    /// </summary>
    public class SampleJob : ClientService
    {
        public SampleJob() : base(new Vendor.api.utils.SessionHelper(null, null))
        {
            // TODO: Add constructor logic here
            //
            //@@CREATE_LOGGERS


        }

        public void HandleJob()
        {
        }






    }

}
