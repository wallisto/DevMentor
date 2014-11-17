using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace YieldCurveInterpolation
{
    public class CubicInterpolation
    {
        List<RateAtDate> sortedSource;

        public List<RateAtDate> SortedSource
        {
            get { return sortedSource; }
        }

        public CubicInterpolation( IEnumerable<RateAtDate> source)
        {
            sortedSource = source.OrderBy(r => r.Date).ToList();
        }

        public double GetRate(DateTime targetDate)
        {
            int before = -1;
            int after = -1;
            int beforeBefore = -1;
            int afterAfter = -1;

            for (int i = 0; i < SortedSource.Count; i++)
            {
                if (SortedSource[i].Date == targetDate)
                    return SortedSource[i].Rate;

                if (SortedSource[i].Date < targetDate)
                {
                    before = i;
                }
                else
                {
                    after = i;
                    break;
                }
            }

            if (before == 0)
            {
                beforeBefore = before;
            }
            else
            {
                beforeBefore = before - 1;
            }

            if (after == SortedSource.Count - 1)
            {
                afterAfter = after;
            }
            else
            {
                afterAfter = after + 1;
            }

            double mu = Utils.GetMu(SortedSource[before], SortedSource[after], targetDate);

            double mu2 = mu * mu;

            double a0 = SortedSource[afterAfter].Rate - SortedSource[after].Rate - SortedSource[beforeBefore].Rate + SortedSource[before].Rate;
            double a1 = SortedSource[beforeBefore].Rate - SortedSource[before].Rate - a0;
            double a2 = SortedSource[after].Rate - SortedSource[beforeBefore].Rate;
            double a3 = SortedSource[before].Rate;

            return (a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3);
        }
    }
}
