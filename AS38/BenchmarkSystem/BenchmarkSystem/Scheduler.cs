using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkSystem
{
    class Scheduler
    {
        private IList<Job> shortJobs;
        private IList<Job> longJobs;
        private IList<Job> veryLongJobs;

        public Scheduler()
        {
            shortJobs = new List<Job>();
            longJobs = new List<Job>();
            veryLongJobs = new List<Job>();
        }

        public void AddJob(Job job)
        {
            if (job.ExpectedRuntime < 30)
            {
                shortJobs.Add(job);
            }else if(job.ExpectedRuntime >= 30 && job.ExpectedRuntime < 120){
                longJobs.Add(job);
            }else{
                veryLongJobs.Add(job);
            }
        }

        public void RemoveJob(Job job)
        {
            if (job.ExpectedRuntime < 30)
            {
                shortJobs.Remove(job);
            }
            else if (job.ExpectedRuntime >= 30 && job.ExpectedRuntime < 120)
            {
                longJobs.Remove(job);
            }
            else
            {
                veryLongJobs.Remove(job);
            }
        }

        public Job PopJob()
        {
            throw new NotImplementedException();
        }
    }
}
