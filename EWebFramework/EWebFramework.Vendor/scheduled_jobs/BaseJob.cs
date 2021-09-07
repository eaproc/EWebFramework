using EPRO.Library.Objects;
using EWebFramework.Vendor.loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.scheduled_jobs
{
    /// <summary>
    /// Summary description for BaseJob
    /// </summary>
    public abstract class BaseJob
    {

        private BaseJob()
        {
            //
            // TODO: Add constructor logic here
            //
            this.Loggers = new List<ILoggable>();
            //this.Loggers.Add(new EPArtisan.Loggers.ConsoleLogger());
            //this.Loggers.Add(new EPArtisan.Loggers.FileLogger());



            this.sbJobInfo = new StringBuilder();
           

        }

        public BaseJob(TimeSpan pExecuteOnTime, JobInterval pJobMode = JobInterval.ONCE, DateTime? pExecuteOnDay = null) : this()
        {

            this.ExecuteOnDay = !pExecuteOnDay.HasValue ? DateTime.Now : pExecuteOnDay.Value;
            this.ExecuteOnTime = pExecuteOnTime; // new TimeSpan(12, 00, 00);
            this.JobMode = pJobMode;



            // set time of execution

            DateTime time = new DateTime(ExecuteOnTime.Ticks);
            DateTime timeExpected = EDateTime.AddDayWithTime(DateTime.Now, time);

            switch (pJobMode)
            {
                case JobInterval.ONCE:
                    this.NextExpectedExecutionTime = DateTime.Now > timeExpected ? EDateTime.AddDayWithTime(DateTime.Now.AddDays(1), time) : timeExpected;
                    break;
                case JobInterval.HALF_HOURLY:
                    this.NextExpectedExecutionTime = DateTime.Now > timeExpected ? EDateTime.AddDayWithTime(DateTime.Now.AddDays(1), time) : timeExpected;
                    break;
                case JobInterval.HOURLY:
                    this.NextExpectedExecutionTime = DateTime.Now > timeExpected ? EDateTime.AddDayWithTime(DateTime.Now.AddDays(1), time) : timeExpected;
                    break;
                case JobInterval.TWO_HOURLY:
                    this.NextExpectedExecutionTime = DateTime.Now > timeExpected ? EDateTime.AddDayWithTime(DateTime.Now.AddDays(1), time) : timeExpected;
                    break;
                case JobInterval.FOUR_HOURLY:
                    this.NextExpectedExecutionTime = DateTime.Now > timeExpected ? DateTime.Now.AddMinutes(5) : timeExpected; //DateTime.Now.AddMinutes(5) Add 4hours
                    break;
                case JobInterval.DAILY:
                    this.NextExpectedExecutionTime = DateTime.Now > timeExpected ? EDateTime.AddDayWithTime(DateTime.Now.AddDays(1), time) : timeExpected;
                    break;
                case JobInterval.WEEKLY:
                    timeExpected = ExecuteOnDay.AddSeconds(0); //Clone

                    timeExpected = EDateTime.AddDayWithTime(timeExpected, time);

                    // the reason for this is so when the job is recreated it meets up for lose time
                    while (DateTime.Now > timeExpected)
                        timeExpected = timeExpected.AddDays(7);

                    this.NextExpectedExecutionTime = timeExpected;


                    break;
                case JobInterval.MONTHLY:

                    timeExpected = ExecuteOnDay.AddSeconds(0); //Clone

                    timeExpected = EDateTime.AddDayWithTime(timeExpected, time);


                    while (DateTime.Now > timeExpected)
                        timeExpected = timeExpected.AddMonths(1);

                    this.NextExpectedExecutionTime = timeExpected;



                    break;
            }



            // Create this job and save in CronJob                
            this.JobID = this.onJobCreating();

        }


        public BaseJob(DateTime pExecuteOnTime) : this()
        {

            this.ExecuteOnDay = pExecuteOnTime.AddSeconds(0);//Clone
            this.ExecuteOnTime = pExecuteOnTime.TimeOfDay; // new TimeSpan(12, 00, 00);
            this.JobMode = JobInterval.ONCE;


            // set time of execution
            this.NextExpectedExecutionTime = pExecuteOnTime;



            // Create this job and save in CronJob                
            this.JobID = this.onJobCreating();

        }



        private DateTime ExecuteOnDay;
        private TimeSpan ExecuteOnTime;
        protected List<ILoggable> Loggers;
        public bool DebugMode;

        private DateTime? LastExecutedOn;
        private DateTime? NextExpectedExecutionTime;
        private JobInterval JobMode;

        /// <summary>
        /// Use to track job with database. Set only on Job Creating
        /// </summary>
        protected readonly int? JobID;
        public abstract String JobName();
        public abstract String JobDescription();

        /// <summary>
        /// Use to store information on current cycle that can be sent to database
        /// </summary>
        protected StringBuilder sbJobInfo;



        /// <summary>
        /// You can create a copy in database here if it hasn't been created
        /// </summary>
        protected abstract int onJobCreating();
        protected abstract void onJobCreated(Exception ex = null);

        protected abstract void onJobTerminated();

        protected abstract void onJobCalling();
        protected abstract void onJobCalled(Exception ex = null);



        public void CreateJob(CronJob cj)
        {
            try
            {
                if (this.HasExpired()) throw new Exception("Invalid Settings! The job has expired!");


                
                cj.AddTask(this, (ushort)this.GetTimeInSecsTillNextTask());

                if (DebugMode) Log("CREATED");
                this.onJobCreated();

            }
            catch (Exception ex)
            {
                // Error occured while calling Job;
                Log(String.Format("ERROR ON CREATION: {1}{0}{2}", Environment.NewLine, ex.Message, ex.StackTrace));
                this.onJobCreated(ex);

            }
        }

        public void JobCalled(CronJob cj)
        {
            //Clear info at this point
            sbJobInfo.Clear();

            try
            {
                this.onJobCalling();

                // Evalute the Job if it need re-schedulling
                // else terminate
                switch (this.JobMode)
                {
                    case JobInterval.ONCE:
                        this.TerminateJob();
                        break;
                    case JobInterval.HALF_HOURLY:
                        this.NextExpectedExecutionTime = DateTime.Now.AddMinutes(30);
                        break;
                    case JobInterval.HOURLY:
                        this.NextExpectedExecutionTime = DateTime.Now.AddHours(1);
                        break;
                    case JobInterval.TWO_HOURLY:
                        this.NextExpectedExecutionTime = DateTime.Now.AddHours(2);
                        break;
                    case JobInterval.FOUR_HOURLY:
                        this.NextExpectedExecutionTime = DateTime.Now.AddHours(4);
                        break;
                    case JobInterval.DAILY:
                        this.NextExpectedExecutionTime = DateTime.Now.AddDays(1);
                        break;
                    case JobInterval.WEEKLY:
                        this.NextExpectedExecutionTime = DateTime.Now.AddDays(7);
                        break;
                    case JobInterval.MONTHLY:

                        this.NextExpectedExecutionTime = DateTime.Now.AddMonths(1);
                        break;
                }

                this.LastExecutedOn = DateTime.Now;

                // reschedule if it is not once
                if (this.JobMode != JobInterval.ONCE)
                    cj.AddTask(this, (ushort)this.GetTimeInSecsTillNextTask());


                if (DebugMode) Log("JOB_CALLED");


                this.onJobCalled();

            }
            catch (Exception ex)
            {

                // Error occured while calling Job;
                Log(String.Format("ERROR ON CALLED: {1}{0}{2}", Environment.NewLine, ex.Message, ex.StackTrace));
                this.onJobCalled(ex:ex);

                //Now Terminate
                this.TerminateJob();
            }
        }


        public bool HasExpired()
        {
            return this.GetTimeInSecsTillNextTask() <= 0;
        }

        private void TerminateJob()
        {
            try
            {
                this.onJobTerminated();
                Log("JOB TERMINATED");

            }
            catch (Exception ex2)
            {
                // Error occured while Terminating Job;
                Log(String.Format("ERROR ON TERMINATION: {1}{0}{2}", Environment.NewLine, ex2.Message, ex2.StackTrace));
            }
        }


        public enum JobInterval
        {
            // if next execution time is already passed set it to tomorrow
            ONCE = 1,            // Executes once Time is Less than or Equal execution time - set's next execution time to today and time
            HALF_HOURLY,     // Sets next execution time to today and time - increment by 30mins
            HOURLY,          // Sets next execution time to today and time - increment by 60mins
            TWO_HOURLY,      // Sets next execution time to today and time - increment by 120mins
            FOUR_HOURLY,     // Sets next execution time to today and time - increment by 240mins
            DAILY,           // Sets next execution time to today and time - increment by 24 hours
            WEEKLY,          // Sets next execution time to Execution Day Set in this week or next week and time - increment by 7days
            MONTHLY          // Sets next execution time to Execution Day Set in this week or next week and time - increment by 1month
        }


        public void Log(String content)
        {
            try
            {
                content = String.Format("{1} Job [{2}]{0}LastExecutedOn: {3}{0}NextExpectedExecutionTime: {4}{0}{5}{0}",
                                Environment.NewLine,
                                this.JobName(),
                                this.JobMode.ToString(),
                                this.LastExecutedOn.HasValue ? this.LastExecutedOn.Value.ToString() : "NA",
                                this.NextExpectedExecutionTime.HasValue ? this.NextExpectedExecutionTime.Value.ToString() : "NA",
                                content
                                );
                foreach (ILoggable Logger in this.Loggers) Logger.Write(content);

            }
            catch (Exception ex)
            {
                // Ignore error on Log
                Logger.Print(ex);
            }

        }

        /// <summary>
        /// Use to store information on current cycle that can be sent to database
        /// </summary>
        /// <param name="content"></param>
        public void Info(string content)
        {
            this.sbJobInfo.AppendLine(content);
        }



        public int GetTimeInSecsTillNextTask()
        {
            return EInt.valueOf(EDouble.valueOf(EDateTime.GetTimeDifferenceInMilliseconds(DateTime.Now, NextExpectedExecutionTime.Value) / 1000));
        }


        public JobInterval GetJobInterval()
        {
            return this.JobMode;
        }

        public DateTime? GetNextExpectedExecutionTime()
        {
            return this.NextExpectedExecutionTime;
        }

    }

}