using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using QuartzSample.DTOs;

namespace QuartzSample.Services
{
    public class JobManager: IJobManager
    {
        private readonly IScheduler _scheduler;
        
        public JobManager(ISchedulerFactory scheduler )
        {
            _scheduler = scheduler.GetScheduler().Result;
         
        }




        public async Task<IEnumerable<GetJobDTO>> GetJobAllJobs()
        {
           List<GetJobDTO> jobs = new List<GetJobDTO>();

           var JobKeys = await   _scheduler.GetJobKeys( GroupMatcher<JobKey>.AnyGroup());

            foreach (var item in JobKeys)
            {
              
                var Trigger=(await _scheduler.GetTriggersOfJob( item )).FirstOrDefault() as SimpleTriggerImpl;
                var job=await _scheduler.GetJobDetail( item );
                if (Trigger != null)
                {
                    jobs.Add(new GetJobDTO()
                    {
                        Jobkey = Trigger.JobName,
                        JobGroup = Trigger.JobGroup,
                        TriggerKey = Trigger.Name,
                        TriggerGroup = Trigger.Group,
                        JobFirstStartTime = Trigger.StartTimeUtc.ToLocalTime(),
                        JobNextRunTime = Trigger.GetNextFireTimeUtc() == null ? DateTimeOffset.Now.ToLocalTime() : Trigger.GetNextFireTimeUtc().Value.ToLocalTime(),
                        JobOldRunTime = Trigger.GetPreviousFireTimeUtc()==null? DateTimeOffset.Now.ToLocalTime(): Trigger.GetPreviousFireTimeUtc().Value.ToLocalTime(),
                        TriggerCountRun = Trigger.TimesTriggered,
                        ScheduleName = _scheduler.SchedulerName,
                        JobState = (await _scheduler.GetTriggerState(Trigger.Key)).ToString()



                    });
                }
                


            }

               return jobs;
        }

        public async Task CreateSchedule(Type Job)
        {
            string Triggername = Job.Name + "Trigger";
            string Jobrname = Job.Name + "Job";
          
            var Triger = await _scheduler.GetTrigger(new TriggerKey(Triggername));
            await _scheduler.AddJob(CreateJob(Job),true);

            if (Triger ==null )
            {
                await _scheduler.ScheduleJob(CreateTriger(Job));
            }

        }

       

        public async Task StopAlljobSchedule()
        {
            await _scheduler.Shutdown();

        }

        public async Task DeleteTrigger(string TriggerKey)
        {
            await _scheduler.UnscheduleJob(new TriggerKey(TriggerKey));
        }
        public async Task DeleteJob(string Jobkey)
        {
            await _scheduler.DeleteJob(new JobKey(Jobkey));
        }

        public async Task PauseTrigger(string TriggerKey)
        {
            await _scheduler.PauseTrigger(new TriggerKey(TriggerKey));
        }

        public async Task ResumeTrigger(string TriggerKey)
        {
            await _scheduler.ResumeTrigger(new TriggerKey(TriggerKey));
        }

        public async Task PauseJob(string Jobkey)
        {
          
            await _scheduler.PauseJob(new JobKey(Jobkey));

        }

        public async Task ResumeJob(string Jobkey)
        {

            await _scheduler.ResumeJob(new JobKey(Jobkey));

        }



        private  IJobDetail CreateJob(Type Job)
        {
           return  JobBuilder.Create(Job).WithIdentity(Job.Name+"Job").StoreDurably().Build();
        }

        private ITrigger CreateTriger(Type Job)
        {
            string Jobrname = Job.Name + "Job";
            return TriggerBuilder.Create().WithIdentity(Job.Name + "Trigger").ForJob(new JobKey(Jobrname)).WithSimpleSchedule(p=>p.WithIntervalInSeconds(2).RepeatForever()).Build();
        }


    }
}
