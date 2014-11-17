using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace DevelopMentor.ObjectPooling
{
    class BasicConnection
    {
        public BasicConnection()
        {
            Thread.Sleep(2000);
        }

        public void DoWork(string message)
        {
            PerfmonCounters.CallsPerSecond.Increment();
            Thread.Sleep(5); //simulate work
        }
    }
}
