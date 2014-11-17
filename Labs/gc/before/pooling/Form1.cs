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

namespace DevelopMentor.ObjectPooling
{
    public partial class Form1 : Form
    {

        volatile bool Cancelled = false;           //used to signal child threads
        List<Thread> threads = new List<Thread>(); //List of child threads

        public Form1()
        {
            InitializeComponent();
            // uncomment this line if you want 
            //to uninstall the perfmon counter.  Seems 
            //unlikely, but provided for your convenience
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

        private void Form1_Load(object sender, EventArgs e)
        {
            SetThreadParameters();
        }
        #endregion

        #region Thread Management

        void WorkerThread()
        {
            while (!Cancelled)
            {
                BasicConnection conn = new BasicConnection();
                conn.DoWork("Hello");
                conn.DoWork("Goodbye");
            }
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
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //TODO: set ReallySimpleConnectionPool.Capacity = trackBar1.Value;
            label1.Text = trackBar1.Value.ToString();
        }
        #endregion
    }
}