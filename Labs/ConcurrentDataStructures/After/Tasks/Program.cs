using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Tasks
{
    class Program
    {
        static void Main()
        {
            const int NUM_CONSUMERS = 10;

            TradeDayProcessor processor = new TradeDayProcessor(NUM_CONSUMERS,
                                                                @"..\..\..\dowjones.csv",
                                                                day => (day.Close - day.Open) / day.Open > 0.05);

            CancellationTokenSource src = new CancellationTokenSource();
            Task cancelTask = new Task(Canceller, src);

            cancelTask.Start();

            Stopwatch sw = Stopwatch.StartNew();
            processor.Start(src.Token);

            int totalMatches = processor.GetMatchingCount();

            Console.WriteLine("Total processing time {0}", sw.Elapsed);

            Console.WriteLine("Total matches {0}", totalMatches);
        }

        private static void Canceller(object o)
        {
            CancellationTokenSource src = (CancellationTokenSource)o;

            Console.WriteLine("press enter to cancel");
            Console.ReadLine();

            src.Cancel();
        }
    }
}
