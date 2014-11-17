using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PurchaseOrderTerminal
{
    public interface ITerminal
    {
        void Clear();
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteLine();
    }

    public class WizzyTerminal : ITerminal
    {

        public WizzyTerminal()
        {
            

        }
        public void Clear()
        {
            Console.Clear();
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format,args);
        }


        public void WriteLine()
        {
            Console.WriteLine();
        }


        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }
    }

    public class VT100 : ITerminal
    {
        const int CHARACTER_DELAY = 20;

        public VT100()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetWindowSize(80, 25);
          
        }
        public void Clear()
        {
            Console.Clear();
        }

        public void WriteLine(string format, params object[] args)
        {
            Write(format, args);   
            Console.WriteLine();
        }


        public void WriteLine()
        {
            WriteLine("");
        }


        public void Write(string format, params object[] args)
        {
            string msg = String.Format(format, args);

            foreach (char ch in msg)
            {
                Console.Write(ch);
                Thread.Sleep(CHARACTER_DELAY);
            }
        }
    }

}
