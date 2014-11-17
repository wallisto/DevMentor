using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Interop
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static string GetRatesFilePath()
        {
            return Path.GetFullPath(@"..\..\rates.csv");
        }
    }
}
