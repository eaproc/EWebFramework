using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;


namespace EWebFramework.Vendor.scheduled_jobs
{



    /// <summary>
    /// Summary description for CronJob
    /// </summary>
    public class CronJob
    {
        public CronJob(params BaseJob[] baseJobs)
        {
            foreach (BaseJob bj in baseJobs) bj.CreateJob(this);
        }

        public void AddTask(BaseJob bjTask, UInt16 seconds)
        {
            CacheItemRemovedCallback OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
            HttpRuntime.Cache.Insert(bjTask.JobName(), bjTask, null,
                DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, OnCacheRemove);
        }



        public void CacheItemRemoved(string taskName, object v, CacheItemRemovedReason r)
        {

            BaseJob bjTask = (BaseJob)v;
            // only process on expiry
            if(r == CacheItemRemovedReason.Expired)
            {
                System.Threading.Thread thread = new System.Threading.Thread( () => bjTask.JobCalled(this) );
                thread.Start();

            }

        }






    }
}
