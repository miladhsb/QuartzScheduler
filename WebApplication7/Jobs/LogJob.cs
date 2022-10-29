using Quartz;
using QuartzSample.Services;

namespace QuartzSample.Jobs
{
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
            logger.LogInformation("logggggggg");
            Console.WriteLine("logggggggg");
            return Task.CompletedTask;
        }
    }
}
