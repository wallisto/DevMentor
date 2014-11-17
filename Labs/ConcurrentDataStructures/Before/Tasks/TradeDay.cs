using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks
{
    class TradeDay
    {
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
        public double AdjClose { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} - {2}", Date, Open, Close);
        }
    }
}
