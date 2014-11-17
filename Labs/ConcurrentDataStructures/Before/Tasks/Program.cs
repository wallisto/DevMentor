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
            const int NUM_CONSUMERS = 1;

            TradeDayProcessor processor = new TradeDayProcessor(NUM_CONSUMERS,
                                                                @"..\..\..\dowjones.csv",
                                                                day => (day.Close - day.Open) / day.Open > 0.05);

            // TODO: Enable cancellation

            Stopwatch sw = Stopwatch.StartNew();

            // TODO: Pass in cancellation token
            processor.Start();

            int totalMatches = processor.GetMatchingCount();

            Console.WriteLine("Total processing time {0}", sw.Elapsed);

            Console.WriteLine("Total matches {0}", totalMatches);
        }

        private static void Canceller(object o)
        {
            // TODO: Implement cancellation
        }
    }
}
