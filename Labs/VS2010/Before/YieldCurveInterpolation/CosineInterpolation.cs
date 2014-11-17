using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace YieldCurveInterpolation
{
    public class CosineInterpolation : TwoItemInterpolation
    {
        public CosineInterpolation(IEnumerable<RateAtDate> source)
            : base(source)
        {
        }

        protected override double InterpolatedRate(RateAtDate before, RateAtDate after, DateTime targetDate)
        {
            double mu = Utils.GetMu(before, after, targetDate); 
            double mu2 = (1 - Math.Cos(mu * Math.PI)) / 2;

            return (before.Rate * (1 - mu2) + after.Rate * mu2);
        }
    }
}
