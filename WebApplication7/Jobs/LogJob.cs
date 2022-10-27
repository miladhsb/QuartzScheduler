using Quartz;

namespace QuartzSample.Jobs
{
    public class LogJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("logggggggg");
            return Task.CompletedTask;
        }
    }
}
