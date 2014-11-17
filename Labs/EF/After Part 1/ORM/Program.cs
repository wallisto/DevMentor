using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pubs = new Pubs())
            {
                foreach (Publisher publisher in pubs.Publishers)
                {
                    Console.WriteLine(publisher);
                }
            }

        }
    }
}
