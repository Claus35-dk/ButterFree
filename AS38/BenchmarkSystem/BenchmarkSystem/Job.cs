using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkSystem
{
    class Job
    {
        public Owner owner
        {
            get;
            set;
        }

        public byte CPU
        {
            get;
            set;
        }

        public int ExpectedRuntime
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public Func<string[], string> process
        {
            get;
            set;
        }
    }
}
