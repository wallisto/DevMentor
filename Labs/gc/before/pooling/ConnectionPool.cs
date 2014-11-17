using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace DevelopMentor.ObjectPooling
{


    //This class implements a simple connection pool with the following features
    //
    //* Maximum Pool Capacity can be adjusted dynamically using the Capacity property
    //
    //* When a new object is needed it is constructed on a background thread so as not to
    //  disrupt later requests.
    //
    //* A watchdog thread trims the pool every two seconds, so if the pool winds up with more
    //  objects in it than needed they will gracefully fall away.

    class ReallySimpleConnectionPool
    {
        #region fields
        //This is the capacity of the pool.
        static int _Capacity = 3;

        //How many objects are in the pool + how many are being constructed
        static int Count;

        //This list just hold the basic connections 
        static List<BasicConnection> Connections = new List<BasicConnection>();

        //This queue is the list of avaiable connections.
        static Queue<BasicConnection> AvailableConnections = new Queue<BasicConnection>();

        //number of threads waiting for an object from the pool
        static int WaitingThreads = 0;


        #endregion

        #region Private implementation details
        static ReallySimpleConnectionPool()
        {
            Thread t = new Thread(WatchdogThread);
            t.IsBackground = true;
            t.Start();
        }


        static void WatchdogThread()
        {
            for (; ; )
            {
                Thread.Sleep(2000);
                lock (AvailableConnections)
                {
                    if (Capacity > 1)
                    {
                        try
                        {

                            if (AvailableConnections.Count > WaitingThreads)
                            {
                                Monitor.Wait(AvailableConnections);

                                BasicConnection conn = AvailableConnections.Dequeue();
                                Connections.Remove(conn);
                                Count--;
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString());
                        }
                    }
                }
            }
        }
        static void CreateNewPooledConnection()
        {
            Debug.WriteLine("Creating new Basic connection...");
            BasicConnection conn = new BasicConnection();

            lock (AvailableConnections)
            {
                Connections.Add(conn);
            }

            PerfmonCounters.BasicConnections.Increment();
            PerfmonCounters.CurrentPoolSize.RawValue = Count;
            DisposeConnection(conn);
        }
        #endregion

        #region public API
        public static int Capacity
        {
            get
            {
                int capacity = 0;
                Interlocked.Exchange(ref capacity, _Capacity);
                return capacity;
            }
            set
            {
                lock (AvailableConnections)
                {
                    _Capacity = value;
                }
            }
        }

        public static BasicConnection GetConnection()
        {
            BasicConnection retval = null;

            lock (AvailableConnections)
            {
                PerfmonCounters.MaxPoolCapacity.RawValue = Capacity;
                PerfmonCounters.CurrentPoolSize.RawValue = Count;

                if (AvailableConnections.Count > 0 && WaitingThreads == 0)
                {
                    //if we have available connections and no one is waiting
                    //then we just return immediately.
                    retval = AvailableConnections.Dequeue();
                }
                else
                {
                    if (AvailableConnections.Count == 0 && Count < Capacity)
                    {
                        Count++;
                        Thread t = new Thread(CreateNewPooledConnection);
                        t.Start();
                    }

                    //If there are no connections available (or there are but
                    //other threads already waiting) then we have to get
                    //in line and wait for one to be released.
                    WaitingThreads++;
                    Monitor.Wait(AvailableConnections);
                    WaitingThreads--;

                    retval = AvailableConnections.Dequeue();

                }
            }

            return retval;
        }

        //NOTE: ONLY CALL THIS WHEN THE PROGRAM IS IDLE
        //This method is only provided for the convenience of the
        //testing harness - it is not thread safe and will wreak
        //havoc if called while the pool is in use;
        public static void Clear()
        {
            Count = 0;
            Connections.Clear();
            AvailableConnections.Clear();
        }

        public static void DisposeConnection(BasicConnection conn)
        {
            lock (AvailableConnections)
            {
                PerfmonCounters.CurrentPoolSize.RawValue = Count;

                //if the capacity has shrunk then we don't
                //put this in the pool. 
                if (Count <= Capacity)
                {
                    AvailableConnections.Enqueue(conn);
                    Monitor.Pulse(AvailableConnections);
                }
                else
                {
                    Count--;
                    Debug.WriteLine("Discarding connection");
                    Connections.Remove(conn);
                }

            }
        }
        #endregion
    }
}
