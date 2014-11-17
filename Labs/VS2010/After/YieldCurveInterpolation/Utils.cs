using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace YieldCurveInterpolation
{
    static class Utils
    {
        internal static double GetMu(RateAtDate before, RateAtDate after, DateTime targetDate)
        {
            Contract.Requires(after.Date != before.Date);
      
            return (targetDate - before.Date).TotalMilliseconds /
                   (after.Date - before.Date).TotalMilliseconds;
        }
    }
}
