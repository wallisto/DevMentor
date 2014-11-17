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

        public List<RateAtDate> SortedSource
        {
            get { return sortedSource; }
        }
        public TwoItemInterpolation(IEnumerable<RateAtDate> source)
        {
            Contract.Requires(source != null);
            Contract.Requires(source.Count() >= 2);

            sortedSource = source.OrderBy(r => r.Date).ToList();
        }

        public double GetRate(DateTime targetDate)
        {
            Contract.Requires(SortedSource != null);
            Contract.Requires(targetDate >= SortedSource[0].Date);
            Contract.Requires(targetDate <= SortedSource[SortedSource.Count - 1].Date);

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
