using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Messaging;
using System.Threading;

namespace Clock
{
    public partial class ClockForm : Form
    {
        const string QueuePath = @".\private$\runaway";

        delegate void UpdateDelegate(DateTime dt);

        public void UpdateUI(DateTime dt)
        {
            if (InvokeRequired)
                BeginInvoke(new UpdateDelegate(UpdateUI),
                    new object[] { dt });
            else
                label1.Text = dt.ToLongTimeString();
        }

        public void ReadThread()
        {
            Thread.Sleep(1000);

            for (; ; )
            {
                try
                {
                    MessageQueue m = new MessageQueue(QueuePath, QueueAccessMode.Receive);
                    System.Messaging.Message msg = m.Receive(new TimeSpan(0, 0, 1));
                    msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(DateTime) } );
                    UpdateUI((DateTime)msg.Body);

                    m.Close();
                }
                catch(Exception e)
                {
                }
            }

        }

        public ClockForm()
        {
            InitializeComponent();

            Thread t = new Thread(ReadThread);
            t.Priority = ThreadPriority.AboveNormal;
            t.Name = "Read Thread";
            t.IsBackground = true;
            t.Start();
        }
    }
}