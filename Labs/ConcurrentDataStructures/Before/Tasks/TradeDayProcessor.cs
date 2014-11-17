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

        // TODO: create blocking collection for holding generated work

        // TODO: Create Task variable to hold reference to generating task

        // TODO: Create list of tasks, returning int, to hold consuming tasks

        // TODO: Create member variable to hold cancellation token

        public TradeDayProcessor(int numConsumers, string tradeFile, Predicate<TradeDay> test)
        {
            this.numConsumers = numConsumers;
            this.tradeFile = tradeFile;
            this.test = test;
        }


        // TODO: Pass in cancellation token
        public void Start()
        {
            // TODO: Store cancellation token

            // TODO: Create and start generation task

            // TODO: Create and start consumering tasks
        }


        public int GetMatchingCount()
        {
            try
            {
                // TODO: Create a list of all tasks both generating and consuming

                // TODO: Wait on all tasks to complete

                // TODO: Loop through the completed tasks
                // summing their results 

                // TODO: return total from consuming tasks
            }
            catch (AggregateException x)
            {
                // TODO: print out the exception message from each 
                // aggregated exception. Remember to call Flatten in
                // case of nested aggregate exceptions
            }

            return -1;
        }
        
        
        private void GenerateTradeDays()
        {
            // TODO: iterate through the ReadStockData() returned iterator
            // and add each item into the blocking collection

            // TODO: Inform consumers that no more items will be 
            // added to the blocking collection
        }


        private int ConsumeTradeDays()
        {
            int matchingDays = 0;

            // TODO: Consume blocking collection

            // TODO: Run test against each item in collection
            // incrementing count if test returns true

            // TODO: return count

            return matchingDays;
        }

        #region Process trade file
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

        #endregion
    }
}
