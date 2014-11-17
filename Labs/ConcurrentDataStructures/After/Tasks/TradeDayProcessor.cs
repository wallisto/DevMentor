using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

namespace Tasks
{
    class TradeDayProcessor
    {
        int numConsumers;
        string tradeFile;
        Predicate<TradeDay> test;

        BlockingCollection<TradeDay> col = new BlockingCollection<TradeDay>(new ConcurrentQueue<TradeDay>());
        Task generateTask;

        List<Task<int>> consumingTasks = new List<Task<int>>();

        CancellationToken cancellationToken;

        public TradeDayProcessor(int numConsumers, string tradeFile, Predicate<TradeDay> test)
        {
            this.numConsumers = numConsumers;
            this.tradeFile = tradeFile;
            this.test = test;
        }

        public void Start(CancellationToken token)
        {
            cancellationToken = token;

            generateTask = new Task(GenerateTradeDays, cancellationToken);
            generateTask.Start();

            for (int i = 0; i < numConsumers; i++)
            {
                var task = new Task<int>(ConsumeTradeDays, cancellationToken);
                consumingTasks.Add(task);
                task.Start();
            }
        }

        public int GetMatchingCount()
        {
            try
            {
                List<Task> tasks = new List<Task>();
                tasks.Add(generateTask);
                tasks.AddRange(consumingTasks);

                Task.WaitAll(tasks.ToArray());

                int total = 0;
                foreach (var item in consumingTasks)
                {
                    total += item.Result;
                }

                return total;
            }
            catch (AggregateException x)
            {
                foreach (var item in x.Flatten().InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }

                return -1;
            }
        }

        private void GenerateTradeDays()
        {
            foreach (var item in ReadStockData())
            {
                cancellationToken.ThrowIfCancellationRequested();
                col.Add(item);
//                Console.WriteLine(item);
            }

            col.CompleteAdding();
        }

        private int ConsumeTradeDays()
        {
            int matchingDays = 0;

            foreach (var item in col.GetConsumingEnumerable(cancellationToken))
            {
                Thread.Sleep(5);

                cancellationToken.ThrowIfCancellationRequested();
                if (test(item))
                {
                    matchingDays++;
                    Console.WriteLine(item);
                }
            }

            return matchingDays;
        }

        private IEnumerable<TradeDay> ReadStockData()
        {
            using (StreamReader sr = new StreamReader(tradeFile))
            {
                string line = null;

                // Move past headings
                sr.ReadLine();

                while ((line = sr.ReadLine()) != null)
                {
                    TradeDay day = ParseTradeEntry(line);
                    yield return day;
                }
            }
        }

        private static TradeDay ParseTradeEntry(string entry)
        {
            string[] items = entry.Split(',');

            TradeDay ret = new TradeDay();
            ret.Date = DateTime.Parse(items[0]);
            ret.Open = double.Parse(items[1], NumberFormatInfo.InvariantInfo);
            ret.High = double.Parse(items[2], NumberFormatInfo.InvariantInfo);
            ret.Low = double.Parse(items[3], NumberFormatInfo.InvariantInfo);
            ret.Close = double.Parse(items[4], NumberFormatInfo.InvariantInfo);
            ret.Volume = long.Parse(items[5], NumberFormatInfo.InvariantInfo);
            ret.AdjClose = double.Parse(items[6], NumberFormatInfo.InvariantInfo);

            return ret;
        }
    }
}
