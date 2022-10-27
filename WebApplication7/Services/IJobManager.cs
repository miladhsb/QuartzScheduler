namespace QuartzSample.Services
{
    public interface IJobManager
    {
        Task CreateSchedule(Type Job);
        Task restartSchedule(Type Job);
        Task StopAlljobSchedule();
        Task StopScheduleWithJob(Type Job);
        Task StopScheduleWithTrigger(Type Job);
    }
}
