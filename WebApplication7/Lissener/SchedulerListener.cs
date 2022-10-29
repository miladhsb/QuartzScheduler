﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzSample.Lissener
{
    public class SchedulerListener : ISchedulerListener
    {
        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job added : {jobDetail.Key.Name}");
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job deleted : {jobKey.Name}");
        }

        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job interruped : {jobKey.Name}");
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job paused : {jobKey.Name}");
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job scheduled  : {trigger.JobKey.Name} with trigger {trigger.Key.Name}");
        }

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Jobs paused on group {jobGroup}");
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job resumed on group  {jobGroup}");
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job unscheduled  :  {triggerKey.Name}");
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"SchedulerError with msg {msg}");
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"SchedulerInStandbyMode");
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"SchedulerShutdown");
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"SchedulerShuttingdown");
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"SchedulerStarted");
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"SchedulerStarting");
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"SchedulingDataCleared");
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"TriggerFinalized : {trigger.Key.Name}");
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"TriggerPaused : {triggerKey.Name}");
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"TriggerResumed : {triggerKey.Name}");
        }

        public async Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"TriggersPaused on group : {triggerGroup}");
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            Debug.WriteLine($"TriggersResumed on group : {triggerGroup}");
        }
    }
}
