using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YieldCurveVisualizer;
using System.IO;
using System.Reflection;
using YieldCurveInterpolation;

namespace VisualizerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RateAtDate[] rates = new RateAtDate[]
            {
                new RateAtDate("10/10/2009", 4.01),
                new RateAtDate("11/10/2009", 4.01),
                new RateAtDate("12/10/2009", 4.02),
                new RateAtDate("17/10/2009", 4.14),
                new RateAtDate("31/10/2009", 4.25),
                new RateAtDate("30/11/2009", 4.35),
                new RateAtDate("28/02/2010", 4.5),
                new RateAtDate("31/05/2010", 4.75),
                new RateAtDate("10/10/2010", 5.13),
                new RateAtDate("10/10/2015", 5.5),
            };

            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    
            Visualizer visualizer = new Visualizer(rootPath + @"\testdata");

            visualizer.Visualize(rates, 3000);


        }
    }
}
