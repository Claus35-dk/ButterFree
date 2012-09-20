using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkSystem {
  public sealed class BenchmarkSystem {
    public event EventHandler<JobEventArgs> JobQueued;
    public event EventHandler<JobEventArgs> JobRemoved;
    public event EventHandler<JobEventArgs> JobStarted;
    public event EventHandler<JobEventArgs> JobTerminated;
    public event EventHandler<JobEventArgs> JobFailed;
    public static readonly BenchmarkSystem instance = new BenchmarkSystem();
    private Scheduler scheduler;
    Dictionary<Scheduler.JobType, byte> running = new Dictionary<Scheduler.JobType, byte>();

    private BenchmarkSystem() {
      scheduler = new Scheduler();
      JobStarted += new EventHandler<JobEventArgs>(benchmarkSystem_start);
      JobTerminated += new EventHandler<JobEventArgs>(benchmarkSystem_end);
      JobFailed += new EventHandler<JobEventArgs>(benchmarkSystem_end);
    }

    public void Submit(Job job) {
      job.SetTimestamp();
      OnJobSubmitted(job);
      scheduler.AddJob(job);
    }

    public void Cancel(Job job) {
      OnJobCancelled(job);
      scheduler.RemoveJob(job);
    }

    public IList<Job> Status() {
      throw new NotImplementedException();
    }

    public void ExecuteAll() {
      throw new NotImplementedException();
    }

    #region EventFunctions

    public void OnJobSubmitted(Job job) {
      if (JobQueued != null)
        JobQueued(this, new JobEventArgs(job));
    }

    public void OnJobCancelled(Job job) {
      if (JobRemoved != null)
        JobRemoved(this, new JobEventArgs(job));
    }

    public void OnJobRunning(Job job) {
      if (JobStarted != null)
        JobStarted(this, new JobEventArgs(job));
    }

    public void OnJobTerminated(Job job) {
      if (JobTerminated != null)
        JobTerminated(this, new JobEventArgs(job));
    }

    public void OnJobFailed(Job job) {
      if (JobFailed != null)
        JobFailed(this, new JobEventArgs(job));
    }
    #endregion

    void benchmarkSystem_start(object sender, JobEventArgs e) {
      running[Scheduler.GetJobType(e.job)]++;
    }

    void benchmarkSystem_end(object sender, JobEventArgs e) {
      running[Scheduler.GetJobType(e.job)]--;
    }
  }
}
