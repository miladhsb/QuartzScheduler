using Quartz;
using QuartzSample.Services;

namespace QuartzSample.Jobs
{
    [DisallowConcurrentExecution]
    public class LogJob : IJob
    {
        private readonly ILogger<LogJob> logger;
        private readonly IJobManager jobManager;

        public LogJob(ILogger<LogJob> logger, IJobManager jobManager)
        {
          
            Console.WriteLine("add scoped");
            this.logger = logger;
            this.jobManager = jobManager;
        }
        public Task Execute(IJobExecutionContext context)
        {

            //var res = jobManager.GetJobAllJobs().Result;
            logger.LogInformation("starting task");
           
            Task.Delay(5000).Wait();

            Console.WriteLine("end task");
            return Task.CompletedTask;
        }
    }
}
