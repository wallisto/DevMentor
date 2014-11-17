using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DevelopMentor.ObjectPooling
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1 && (args[0] == "/u" || args[0] == "-u"))
            {
                PerfmonCounters.UninstallPerformanceCounters();
                MessageBox.Show("Uninstalled perfmon counters");
            }
            else
            {
                PerfmonCounters.InitializePerformanceCounters();

                Application.EnableVisualStyles();
                Application.Run(new Form1());
            }
        }
    }
}