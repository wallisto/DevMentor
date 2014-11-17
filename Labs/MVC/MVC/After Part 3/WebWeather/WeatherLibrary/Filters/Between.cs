using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherLibrary.Filters
{
    public static partial class SampleFilters
    {
        public static IEnumerable<WeatherSample> Between(this IEnumerable<WeatherSample> samples, DateTime from, DateTime to)
        {
            return samples.Where(sample =>
                {
                    DateTime sampleDate = new DateTime(sample.Year, sample.Month, 1);

                    return ((from <= sampleDate) && (to <= sampleDate));
                });
        }
    }
}
