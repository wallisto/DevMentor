using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;

namespace DevelopMentor.GCExplorer
{
    public partial class Form1 : Form
    {

        volatile bool Cancelled = false;           //used to signal child threads
        List<Thread> threads = new List<Thread>(); //List of child threads

        long MaxPower; //maximum size of lists allocated (power of 2, so if this is 10 the biggest possible list is 2^10 (1024) objects
        long MinPower; //maximum size of lists allocated (power of 2, so if this is 10 the biggest possible list is 2^10 (1024) objects
        long DumpThreshold; //How often to recycle array entries (e.g. if this is set to 10 then only 10 array entries are actively used).

        public Form1()
        {
            InitializeComponent();
            InitializePerformanceCounters();

            // uncomment this line if you want 
            //to uninstall the perfmon counter.  Seems 
            //unlikely, but provided for your convenience
            //UninstallPerformanceCounters();
        }

        #region event handlers
        private void GCCollect_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void Perfmon_Click(object sender, EventArgs e)
        {
            Process.Start(@"..\..\perfmon.msc");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            threads.Clear();

            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = "Starting threads...";

            Cancelled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;

            int maxThreads = int.Parse(textBox1.Text);
            toolStripProgressBar1.Maximum = maxThreads;
            toolStripProgressBar1.Value = 0;

            Application.DoEvents();

            for (int i = 0; i < maxThreads; i++)
            {
                BeginInvoke(new ThreadStart(StartAThread));
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Cancelled = true;
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = "Stopping threads...";

            Application.DoEvents();

            for (int i = 0; i < threads.Count; i++)
                this.BeginInvoke(new ThreadStart(StopAThread));
        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar2.Value = Math.Max(trackBar1.Value, trackBar2.Value);
            SetThreadParameters();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            trackBar1.Value = Math.Min(trackBar1.Value, trackBar2.Value);
            SetThreadParameters();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            SetThreadParameters();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetThreadParameters();
        }
        #endregion

        #region Thread Management
        Node AllocateList(long len)
        {
            Node root = new Node();
            Node n = root;

            for (int i = 0; i < len - 1; i++)
            {
                n.Next = new Node();
                n = n.Next;
                if (i % 100000 == 0)
                {
                    Thread.Sleep(2); //just to keep from TOTALLY overwhelming the CPU
                    if (Cancelled)
                        break;
                }
            }

            return root;
        }


        void WorkerThread()
        {
            Random r = new Random();

            //we keep up to 100 lists around. Keeping more
            //means more get promoted to gen2
            Node[] lists = new Node[100];
            int n = 0;
            
            long lastThreshold = 0;

            while (!Cancelled)
            {
                long MaxPow = Interlocked.Read(ref MaxPower);
                long MinPow = Interlocked.Read(ref MinPower);

                MinPow = Math.Min(MinPow, MaxPow);

                long pow = r.Next((int)MaxPow - (int)MinPow) + MinPow;

                long threshold = Interlocked.Read(ref DumpThreshold);
                if (threshold < lastThreshold)
                {
                    for (int i = (int)threshold; i < lists.Length; i++)
                        lists[i] = null;
                }

                lastThreshold = threshold;

                Node head = AllocateList((long)Math.Pow(2.0, pow));

                if (threshold > 0)
                    lists[n % threshold] = head;
                n = n+1;
            }

            Debug.WriteLine("WorkerThread exiting");
        }


        void StartAThread()
        {
            Debug.WriteLine("StartAThread()");

            Thread t = new Thread(WorkerThread);
            t.IsBackground = true;
            threads.Add(t);

            int maxThreads = int.Parse(textBox1.Text);
            toolStripProgressBar1.Value++;

            if (threads.Count == maxThreads)
            {
                OnThreadsStarted();
            }

            t.Start();
            Thread.Sleep(75);
        }


        void StopAThread()
        {
            threads[threads.Count - 1].Join();
            threads.RemoveAt(threads.Count - 1);

            toolStripProgressBar1.Value++;

            if (threads.Count == 0)
            {
                OnThreadsStopped();
            }
            Thread.Sleep(75);
        }

        private void OnThreadsStopped()
        {
            this.Cursor = Cursors.Arrow;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Text = "Idle";
            toolStripStatusLabel1.Visible = true;

            button3.Enabled = true;
            button4.Enabled = false;
            textBox1.Enabled = true;
        }

        private void OnThreadsStarted()
        {
            button4.Enabled = true;
            this.Cursor = Cursors.Arrow;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel1.Text = "Running";

        }

        void SetThreadParameters()
        {
            long newMin = trackBar1.Value;
            label2.Text = ((long)Math.Pow(2, newMin)).ToString();
            Interlocked.Exchange(ref MinPower, newMin);

            long newMax = trackBar2.Value;
            label3.Text = ((long)Math.Pow(2, newMax)).ToString();
            Interlocked.Exchange(ref MaxPower, newMax);

            long dumpThreshold = trackBar3.Value * 5;
            label6.Text = dumpThreshold.ToString();
            Interlocked.Exchange(ref DumpThreshold, dumpThreshold);
        }
        #endregion

        #region Perfmon Counter management
        const string PerformanceCategoryName = "Generation Explorer";
        const string PerformanceCounterName = "# objects/sec";
        public static PerformanceCounter AllocationsPerSec;

        static void InstallPerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists(PerformanceCategoryName))
            {

                CounterCreationData CreationData = new CounterCreationData();
                CreationData.CounterName = PerformanceCounterName;
                CreationData.CounterHelp =
                  "Rate at which objects being allocated by Generation Explorer";
                CreationData.CounterType =
                  PerformanceCounterType.RateOfCountsPerSecond32;

                CounterCreationData[] CounterData = { CreationData };

                PerformanceCounterCategory.Create(PerformanceCategoryName, 
                    "DevelopMentor Generation Explorer Counters", 
                    PerformanceCounterCategoryType.MultiInstance,
                     new CounterCreationDataCollection(CounterData));
            }
        }

        static void UninstallPerformanceCounters()
        {
            if (PerformanceCounterCategory.Exists(PerformanceCategoryName))
            {
                PerformanceCounterCategory.Delete(PerformanceCategoryName);
            }
        }


        static void InitializePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists(PerformanceCategoryName))
                InstallPerformanceCounters();

            string exename = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            AllocationsPerSec = new PerformanceCounter(PerformanceCategoryName, PerformanceCounterName, exename, false);
        }
        #endregion

    }


    //this class doesn't do anything besides occupy space.
    public class Node
    {
        public Node Next;
        double a;
        double b;
        double c;
        double d;

        public Node()
        {
            Form1.AllocationsPerSec.Increment();
        }
    }

}