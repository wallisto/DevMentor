using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DebugException
{
    public partial class Form1 : Form
    {
        string DiaryPath = "diary.txt";

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            using (FileStream DiaryFile = new FileStream(DiaryPath, FileMode.Append, FileAccess.Write)) { }

            RefreshDisplay();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileStream DiaryFile = new FileStream(DiaryPath, FileMode.Open, FileAccess.Write))
                {

                    DiaryFile.Seek(0, SeekOrigin.End);

                    using (StreamWriter sw = new StreamWriter(DiaryFile))
                    {
                        sw.WriteLine(DateTime.Now.ToString());
                        sw.WriteLine("----------------------------");
                        sw.WriteLine(textBox2.Text);
                        sw.WriteLine(Environment.NewLine);
                        sw.Flush();
                    }
                    textBox2.Clear();
                    RefreshDisplay();

                }
            }
            catch (Exception ex)
            {
                throw new DiaryException("Please contact technical support", ex);
            }

        }

        void RefreshDisplay()
        {
            textBox1.Clear();

            using (FileStream DiaryFile = new FileStream(DiaryPath, FileMode.Open, FileAccess.ReadWrite))
            {
                DiaryFile.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(DiaryFile))
                {
                    textBox1.Text = reader.ReadToEnd();
                }
            }
        }

        private void clearEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(DiaryPath);
            fi.Delete();

            textBox1.Clear();
        }

    }

    
    [global::System.Serializable]
    public class DiaryException : Exception
    {

        Exception RootException;

        public DiaryException() { }
        public DiaryException(string message) : base(message) { }
        public DiaryException(string message, Exception inner) : base(message)
        {
            RootException = inner;
        }
        protected DiaryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}