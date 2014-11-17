using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace DevelopMentor.ObjectPooling
{
    class PooledConnection : IDisposable
    {
        BasicConnection _Conn = ReallySimpleConnectionPool.GetConnection();
        bool _Disposed = false;


        public void DoWork(string msg)
        {
            if (_Disposed)
                throw new ObjectDisposedException("Error: PooledConnection used after Dispose");
            _Conn.DoWork(msg);
        }

        public void Dispose()
        {
            if (!_Disposed)
            {
                _Disposed = true;
                
                GC.SuppressFinalize(this);
                ReallySimpleConnectionPool.DisposeConnection(_Conn);
            }
        }

        ~PooledConnection()
        {
            try
            {
                ReallySimpleConnectionPool.DisposeConnection(_Conn);
            }
            catch 
            {
                //At program shutdown we have no way of knowing the
                //state of the connection pool.
            }
        }
    }
}
