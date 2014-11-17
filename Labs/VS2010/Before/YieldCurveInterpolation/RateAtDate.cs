using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YieldCurveInterpolation
{
    public struct RateAtDate
    {
        public RateAtDate(DateTime date, double rate)
            : this()
        {
            Date = date;
            Rate = rate;
        }

        public RateAtDate(string date, double rate)
            : this()
        {
            Date = DateTime.Parse(date);
            Rate = rate;
        }

        public DateTime Date { get; set; }
        public double Rate { get; set; }
    }
}
