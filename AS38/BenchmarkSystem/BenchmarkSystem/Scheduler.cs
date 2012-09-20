using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkSystem {
  class Scheduler {
    public enum JobType {
      Short,
      Long,
      VeryLong
    }
    Dictionary<JobType, IList<Job>> jobs = new Dictionary<JobType, IList<Job>>();

    public Scheduler() {
      foreach (JobType type in Enum.GetValues(typeof(JobType))) {
        jobs.Add(type, new List<Job>());
      }
    }

    public void AddJob(Job job) {
      jobs[GetJobType(job)].Add(job);
    }

    public void RemoveJob(Job job) {
      jobs[GetJobType(job)].Remove(job);
    }

    public Job PopJob() {
      Job jobToRun = null;
      foreach (IList<Job> list in jobs.Values) {
        if (jobToRun == null || list[0].timestamp < jobToRun.timestamp) jobToRun = list[0];
      }
      return jobToRun;
    }

    public static JobType GetJobType(Job job) {
      if (job.ExpectedRuntime < 30) {
        return JobType.Short;
      } else if (job.ExpectedRuntime >= 30 && job.ExpectedRuntime < 120) {
        return JobType.Long;
      } else {
        return JobType.VeryLong;
      }
    }
  }
}
