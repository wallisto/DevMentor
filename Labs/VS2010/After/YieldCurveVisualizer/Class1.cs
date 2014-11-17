using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YieldCurveInterpolation;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace YieldCurveVisualizer
{
    public class Visualizer
    {
        string fileRoot;

        public Visualizer(string fileRoot)
        {
            this.fileRoot = fileRoot;
        }

        public void Visualize(IEnumerable<RateAtDate> data, int numPoints)
        {
            const string LinearFile = "Lin.csv";
            const string CosineFile = "Cos.csv";
            const string CubicFile = "Cub.csv";

            DateTime start = data.First().Date;

            LinearInterpolation lin = new LinearInterpolation(data);
            CosineInterpolation cos = new CosineInterpolation(data);
            CubicInterpolation cub = new CubicInterpolation(data);

            using (StreamWriter linsw = new StreamWriter(fileRoot + LinearFile))
            using (StreamWriter cossw = new StreamWriter(fileRoot + CosineFile))
            using (StreamWriter cubsw = new StreamWriter(fileRoot + CubicFile))
            {
                double increment = ((double)(data.Last().Date - data.First().Date).TotalMilliseconds) / (double)numPoints;
                for (int i = 0; i < numPoints; i++)
                {
                    DateTime writeDate = start + TimeSpan.FromMilliseconds((i * increment));
                    linsw.WriteLine(lin.GetRate(writeDate));
                    cossw.WriteLine(cos.GetRate(writeDate));
                    cubsw.WriteLine(cub.GetRate(writeDate));
                }
            }

            ShowData(fileRoot + LinearFile);
            ShowData(fileRoot + CosineFile);
            ShowData(fileRoot + CubicFile);
        }

        private void ShowData(string file)
        {
            Application app = new Microsoft.Office.Interop.Excel.Application();

            Workbooks books = app.Workbooks;
            Workbook book = books.Open(file);

            app.Visible = true;

            Worksheet ws = (Worksheet)book.Worksheets[1];

            CreateGraph(ws);

            Marshal.ReleaseComObject(ws);
            Marshal.ReleaseComObject(book);
            Marshal.ReleaseComObject(books);
        }

        private static void CreateGraph(Worksheet ws)
        {
            ChartObjects xlCharts = ws.ChartObjects();
            ChartObject myChart = (ChartObject)xlCharts.Add(100, 80, 700, 400);
            Chart chartPage = myChart.Chart;

            Range chartRange = ws.get_Range("A:A,B:B");
            chartPage.SetSourceData(chartRange);
            chartPage.ChartType = XlChartType.xlArea;

            Marshal.ReleaseComObject(xlCharts);
            Marshal.ReleaseComObject(myChart);
            Marshal.ReleaseComObject(chartPage);
            Marshal.ReleaseComObject(chartRange);
        }
    }
}
