using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace snotepad
{
    public partial class Form1 : Form
    {
        string CurrentPath = null; //current file being edited
        bool Dirty = false;        //dirty bit

        public Form1()
        {
            InitializeComponent();
            NewFile();
            UpdateUI();
        }

        void OpenFile()
        {
            if (ResolveUnsavedChanges())
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Text Files|*.txt";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(dlg.FileName);
                }
            }
        }

        void NewFile()
        {
            if (ResolveUnsavedChanges())
            {
                textBox1.Clear();
                CurrentPath = null;
                Dirty = false;
            }
        }

        bool Save()
        {
            if (CurrentPath == null)
                return SaveAs();
            else
            {
                SaveFile(CurrentPath);
                return true;
            }
        }

        bool SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (CurrentPath != null)
                dlg.FileName = CurrentPath;
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                SaveFile(dlg.FileName);
                return true;
            }
            else if (res == DialogResult.Cancel)
                return false;
            else
                return true;
        }

        void OpenFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    textBox1.Text = reader.ReadToEnd();

                    Dirty = false;
                    CurrentPath = path;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void SaveFile(string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.Write(textBox1.Text);
                    writer.Flush();

                    Dirty = false;
                    CurrentPath = path;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        bool ResolveUnsavedChanges()
        {
            bool retval = true;

            string filename;
            if (CurrentPath == null)
                filename = "Untitled";
            else
            {
                filename = Path.GetFileName(CurrentPath);
            }

            if (Dirty)
            {
                DialogResult res = MessageBox.Show(string.Format("The text in the file '{0}' has changed{1}{1}Do you want to save the changes?", filename, Environment.NewLine), "SNotepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                    retval = Save();
                else if (res == DialogResult.Cancel)
                    retval = false;
            }

            return retval;
        }

        private void UpdateUI()
        {
            Text = (CurrentPath!=null)? CurrentPath :
                "Untitled" + " - SNotepad";
            if (Dirty)
                Text += " *";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = !ResolveUnsavedChanges();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
            UpdateUI();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            UpdateUI();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
            UpdateUI();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            NewFile();
            UpdateUI();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
            UpdateUI();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save();
            UpdateUI();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Dirty = true;
            UpdateUI();
        }

        private void gCCollectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            toolStripStatusLabel1.Text = string.Format("Collecting Garbage...", GC.CollectionCount(0));

            Timer timer1 = new Timer();
            timer1.Interval = 2000;
            timer1.Tick += delegate { toolStripStatusLabel1.Text = ""; timer1.Stop(); };
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}