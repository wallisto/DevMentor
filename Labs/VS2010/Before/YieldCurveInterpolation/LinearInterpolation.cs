using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace YieldCurveInterpolation
{
    public class LinearInterpolation : TwoItemInterpolation
    {
        public LinearInterpolation(IEnumerable<RateAtDate> source)
            : base (source)
        {
        }

        protected override double InterpolatedRate(RateAtDate before, RateAtDate after, DateTime targetDate)
        {
            double mu = Utils.GetMu(before, after, targetDate);
            return before.Rate * (1 - mu) + after.Rate * mu;
        }
    }
}
