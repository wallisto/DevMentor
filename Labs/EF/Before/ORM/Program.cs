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
            Pubs pubs = new Pubs();

            using (pubs)
            {
                foreach (Publisher pub in pubs.Publishers)
                {
                    Console.WriteLine(pub);
                }
                Console.ReadLine();
            }
        }
    }
}
