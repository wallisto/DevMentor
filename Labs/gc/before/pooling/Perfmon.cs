using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace DevelopMentor.ObjectPooling
{
    class PerfmonCounters
    {
        const string PerformanceCategoryName = "DevelopMentor Object Pooling";
        const string PerformanceCounterName = "# calls/sec";
        const string PerformanceCounterName2 = "# Total Connections created";
        const string PerformanceCounterName3 = "Object Pool Capacity";
        const string PerformanceCounterName4 = "Current Pool Size";

        public static PerformanceCounter CallsPerSecond;
        public static PerformanceCounter BasicConnections;
        public static PerformanceCounter MaxPoolCapacity;
        public static PerformanceCounter CurrentPoolSize;

        static void InstallPerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists(PerformanceCategoryName))
            {

                CounterCreationData CreationData1 = new CounterCreationData();
                CreationData1.CounterName = PerformanceCounterName;
                CreationData1.CounterHelp =
                  "Number of calls executing per second";
                CreationData1.CounterType =
                  PerformanceCounterType.RateOfCountsPerSecond32;

                CounterCreationData CreationData2 = new CounterCreationData();
                CreationData2.CounterName = PerformanceCounterName2;
                CreationData2.CounterHelp =
                  "Total number of Basic Connection objects created";
                CreationData2.CounterType =
                  PerformanceCounterType.NumberOfItems32;

                CounterCreationData CreationData3 = new CounterCreationData();
                CreationData3.CounterName = PerformanceCounterName3;
                CreationData3.CounterHelp =
                  "Max capacity of the object pool";
                CreationData3.CounterType =
                  PerformanceCounterType.NumberOfItems32;

                CounterCreationData CreationData4 = new CounterCreationData();
                CreationData4.CounterName = PerformanceCounterName4;
                CreationData4.CounterHelp =
                  "Current size of the object pool";

                CounterCreationData[] CounterData = { CreationData1, CreationData2, CreationData3, CreationData4 };

                PerformanceCounterCategory.Create(PerformanceCategoryName, 
                    "DevelopMentor Object Pooling Performance Counters", 
                    PerformanceCounterCategoryType.MultiInstance,
                     new CounterCreationDataCollection(CounterData));
            }
        }

        public static void UninstallPerformanceCounters()
        {
            if (PerformanceCounterCategory.Exists(PerformanceCategoryName))
            {
                PerformanceCounterCategory.Delete(PerformanceCategoryName);
            }
        }


        public static void InitializePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists(PerformanceCategoryName))
                InstallPerformanceCounters();
            
            string exename = Path.GetFileName(Assembly.GetEntryAssembly().Location);
            CallsPerSecond = new PerformanceCounter(PerformanceCategoryName, PerformanceCounterName, exename, false);
            BasicConnections = new PerformanceCounter(PerformanceCategoryName, PerformanceCounterName2, exename, false);
            MaxPoolCapacity = new PerformanceCounter(PerformanceCategoryName, PerformanceCounterName3, exename, false);
            CurrentPoolSize = new PerformanceCounter(PerformanceCategoryName, PerformanceCounterName4, exename, false);
        }
    }
}
