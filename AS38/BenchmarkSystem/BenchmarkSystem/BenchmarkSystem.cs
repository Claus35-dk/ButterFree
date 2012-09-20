using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkSystem
{
    class BenchmarkSystem
    {
        public event EventHandler<JobEventArgs> JobSubmitted;
        public event EventHandler<JobEventArgs> JobCancelled;
        public event EventHandler<JobEventArgs> JobRunning;
        public event EventHandler<JobEventArgs> JobTerminated;
        public event EventHandler<JobEventArgs> JobFailed;

        private Scheduler scheduler;

        public void Submit(Job job)
        {
            OnJobSubmitted(job);
            scheduler.AddJob(job);
        }

        public void Cancel(Job job)
        {
            OnJobCancelled(job);
            scheduler.RemoveJob(job);
        }

        public IList<Job> Status()
        {
            throw new NotImplementedException();
        }

        public void ExecuteAll()
        {
            throw new NotImplementedException();
        }

        #region EventFunctions

        public void OnJobSubmitted(Job job)
        {
            if (JobSubmitted != null)
                JobSubmitted(this, new JobEventArgs(job));
        }

        public void OnJobCancelled(Job job)
        {
            if (JobCancelled != null)
                JobCancelled(this, new JobEventArgs(job));
        }

        public void OnJobRunning(Job job)
        {
            if (JobRunning != null)
                JobRunning(this, new JobEventArgs(job));
        }

        public void OnJobTerminated(Job job)
        {
            if (JobTerminated != null)
                JobTerminated(this, new JobEventArgs(job));
        }

        public void OnJobFailed(Job job)
        {
            if (JobFailed != null)
                JobFailed(this, new JobEventArgs(job));
        }

        #endregion
    }
}
