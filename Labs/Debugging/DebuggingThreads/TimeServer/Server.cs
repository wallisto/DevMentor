using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Threading;
using System.Diagnostics;

namespace TimeServer
{
    class App
    {
        const string QueuePath = @".\private$\runaway";
        static object QueueLock = new object();
        static volatile bool Cancelled = false;

        static void QueueThread()
        {
            while (!Cancelled)
            {
                lock (QueueLock)
                {
                    using (MessageQueue mq = new MessageQueue(QueuePath, true))
                    {
                        mq.Send(DateTime.Now);
                        mq.Close();
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            if (!MessageQueue.Exists(QueuePath))
                MessageQueue.Create(QueuePath);

            Thread t = new Thread(QueueThread);
            t.IsBackground = true;
            t.Start();

            Console.WriteLine("Press enter to stop the server");
            Console.ReadLine();

            Cancelled = true;
            t.Join();

            MessageQueue.Delete(QueuePath);
        }
    }
}
