using Quartz;
using Quartz.Impl;

namespace QuartzSample.Services
{
    public class JobManager: IJobManager
    {
        private readonly IScheduler _scheduler;
        
        public JobManager(ISchedulerFactory schedulerFactory )
        {
            _scheduler = schedulerFactory.GetScheduler().Result; ;

             _scheduler.Start().Wait();

        }

         public async Task CreateSchedule(Type Job)
        {
            string Triggername = Job.Name + "Trigger";
            string Jobrname = Job.Name + "Job";
            var job = await _scheduler.GetJobDetail(new JobKey(Jobrname));
            var Triger = await _scheduler.GetTrigger(new TriggerKey(Triggername));

            if (job == null && Triger ==null )
            {
                await _scheduler.ScheduleJob(CreateJob(Job), CreateTriger(Job));
            }
           
         

        }

        public async Task restartSchedule(Type Job)
        {
            string Triggername = Job.Name + "Trigger";
            string Jobrname = Job.Name + "Job";
           await _scheduler.AddJob(CreateJob(Job),true);
           await _scheduler.ScheduleJob( CreateTriger(Job));

        }

        public async Task StopAlljobSchedule()
        {
            await _scheduler.Shutdown();

        }

        public async Task StopScheduleWithTrigger(Type Job)
        {
            string Triggername = Job.Name + "Trigger";
            await _scheduler.UnscheduleJob(new TriggerKey(Triggername));

        }

        public async Task StopScheduleWithJob(Type Job)
        {
            string Jobrname = Job.Name + "Job";
            await _scheduler.DeleteJob(new JobKey(Jobrname));

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
