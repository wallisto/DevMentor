using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections;

namespace YieldCurveInterpolation
{
    public abstract class TwoItemInterpolation
    {
        List<RateAtDate> sortedSource;

        public TwoItemInterpolation(IEnumerable<RateAtDate> source)
        {
            sortedSource = source.OrderBy(r => r.Date).ToList();
        }

        public double GetRate(DateTime targetDate)
        {
            RateAtDate before = new RateAtDate();
            RateAtDate after = new RateAtDate();

            foreach (var item in sortedSource)
            {
                if (item.Date == targetDate)
                {
                    return item.Rate;
                }

                if (item.Date < targetDate)
                {
                    before = item;
                }
                else
                {
                    after = item;
                    break;
                }
            }

            return InterpolatedRate(before, after, targetDate);
        }

        protected abstract double InterpolatedRate(RateAtDate before, RateAtDate after, DateTime targetDate);
    }
}
