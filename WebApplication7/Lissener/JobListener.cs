﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzSample.Lissener
{
    public class JobListener : IJobListener
    {
        public string Name => "Test job listener";

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job vetoed : {context.JobDetail.Key.Name}");
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job is to be executed : {context.JobDetail.Key.Name}");
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.WriteLine($"Job executed : {context.JobDetail.Key.Name}");
        }
    }
}
