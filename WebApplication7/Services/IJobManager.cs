using QuartzSample.DTOs;

namespace QuartzSample.Services
{
    public interface IJobManager
    {
        Task CreateSchedule(Type Job);
        Task DeleteJob(string Jobkey);
        Task DeleteTrigger(string TriggerKey);
        Task<IEnumerable<GetJobDTO>> GetJobAllJobs();
        Task PauseJob(string Jobkey);
        Task PauseTrigger(string TriggerKey);
        Task ResumeJob(string Jobkey);
        Task ResumeTrigger(string TriggerKey);
        Task StopAlljobSchedule();
    }
}
