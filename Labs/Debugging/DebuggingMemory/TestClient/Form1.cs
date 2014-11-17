using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace TestClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void SetInputState(bool running)
        {
            toolStripProgressBar1.Visible = running;
            button2.Enabled = running;
            button1.Enabled = !running;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text.IndexOf("http") == 0)
            {
                SetInputState(true);
                Url = textBox1.Text;
                backgroundWorker1.RunWorkerAsync();
            }
            else
                MessageBox.Show("Run the web project and enter the URL here");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetInputState(false);
            backgroundWorker1.CancelAsync();
        }

        string Url;
        long TotalRequests;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                WebRequest req = WebRequest.Create(Url);
                req.GetResponse().Close();
                Interlocked.Increment(ref TotalRequests);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            long requests = Interlocked.Read(ref TotalRequests);
            toolStripStatusLabel1.Text = requests.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"..\..\..");
            Process.Start(@"C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0\webdev.webserver20.exe", @"/port:19343 /path:" + di.FullName);
            
            button3.Enabled = false;
            button1.Enabled = true;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(textBox1.Text);
        }
    }
}