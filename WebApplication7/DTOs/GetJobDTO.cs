namespace QuartzSample.DTOs
{
    public class GetJobDTO
    {
        public string ScheduleName { get; set; }
        public string Jobkey { get; set; }
        public string JobGroup { get; set; }
        public string TriggerKey { get; set; }
        public string TriggerGroup { get; set; }
        public DateTimeOffset JobFirstStartTime { get; set; }
        public DateTimeOffset JobOldRunTime { get; set; }
        public DateTimeOffset JobNextRunTime { get; set; }
        public int TriggerCountRun { get; set; }
        public string JobState { get; set; }



    }
}
