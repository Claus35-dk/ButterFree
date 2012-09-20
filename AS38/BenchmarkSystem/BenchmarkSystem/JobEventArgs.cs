using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkSystem
{
    class JobEventArgs : EventArgs
    {
        public JobEventArgs(Job job)
        {
            this.job = job;
        }

        public Job job
        {
            get;
            private set;
        }
    }
}
