using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace Interop
{
    class Program
    {
        static void Main(string[] args)
        {
            // Open Excel
            Application xl = new Application();
            Workbooks books = xl.Workbooks;

            // Get the location of the rates file as path is relative to Excel
            string ratesFile = GetRatesFilePath();

            // Open the rates file in Excel
            Workbook book = books.Open(ratesFile);

            dynamic sheet = book.Worksheets[1];

            // Set the format of the first column to date
            Range range = sheet.Range("A:A");
            range.NumberFormat = "yyyy-mm-dd";

            // Create a new chart
            ChartObjects xlCharts = sheet.ChartObjects;
            ChartObject myChart = xlCharts.Add(100, 80, 700, 400);
            Chart chartPage = myChart.Chart;

            // Set the data for the chart and chart type
            Range chartRange = sheet.Range("A:A,B:B");
            chartPage.SetSourceData(chartRange);
            chartPage.ChartType = XlChartType.xlArea;

            // Show Excel to the world
            xl.Visible = true;

            Console.WriteLine("Press enter to quit Excel ...");
            Console.ReadLine();

            // Tell Excel to shutdown
            xl.Visible = false;
            xl.Quit();

            // Release all acquired COM resources to allow the 
            // Excel proces to terminate
            Marshal.ReleaseComObject(chartRange);
            Marshal.ReleaseComObject(chartPage);
            Marshal.ReleaseComObject(myChart);
            Marshal.ReleaseComObject(xlCharts);
            Marshal.ReleaseComObject(range);
            Marshal.ReleaseComObject(sheet);
            Marshal.ReleaseComObject(book);
            Marshal.ReleaseComObject(books);
            Marshal.ReleaseComObject(xl);
        }

        private static string GetRatesFilePath()
        {
            return Path.GetFullPath(@"..\..\rates.csv");
        }
    }
}
