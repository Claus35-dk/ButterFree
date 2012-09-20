using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkSystem
{
    class Logger
    {
        public Logger(BenchmarkSystem benchmarkSystem)
        {
            benchmarkSystem.JobSubmitted += new EventHandler<JobEventArgs>(benchmarkSystem_JobSubmitted);
            benchmarkSystem.JobCancelled += new EventHandler<JobEventArgs>(benchmarkSystem_JobCancelled);
            benchmarkSystem.JobRunning += new EventHandler<JobEventArgs>(benchmarkSystem_JobRunning);
            benchmarkSystem.JobTerminated += new EventHandler<JobEventArgs>(benchmarkSystem_JobTerminated);
            benchmarkSystem.JobFailed += new EventHandler<JobEventArgs>(benchmarkSystem_JobFailed);
        }


        void benchmarkSystem_JobSubmitted(object sender, JobEventArgs e)
        {
            Console.WriteLine("Job Submitted: " + e.job);
        }

        void benchmarkSystem_JobCancelled(object sender, JobEventArgs e)
        {
            Console.WriteLine("Job Cancelled: " + e.job);
        }

        void benchmarkSystem_JobRunning(object sender, JobEventArgs e)
        {
            Console.WriteLine("Job Running: " + e.job);
        }

        void benchmarkSystem_JobTerminated(object sender, JobEventArgs e)
        {
            Console.WriteLine("Job Terminated: " + e.job);
        }
        
        void benchmarkSystem_JobFailed(object sender, JobEventArgs e)
        {
            Console.WriteLine("Job Failed: " + e.job);
        }
    }
}
