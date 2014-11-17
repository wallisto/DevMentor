using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pubs = new Pubs())
            {
                //ListPublishers(pubs);

                ListPublishersAndTitles(pubs);
            }

        }

        private static void ListPublishersAndTitles(Pubs pubs)
        {
            pubs.Configuration.LazyLoadingEnabled = false;

            foreach (Publisher publisher in pubs.Publishers.Include(p => p.Titles))
            {
                Console.WriteLine(publisher);
                foreach (Title title in publisher.Titles)
                {
                    Console.WriteLine("\t{0} @ {1:C}",title.Name,title.Price);
                }
            }
        }

        private static void ListPublishers(Pubs pubs)
        {
            foreach (Publisher publisher in pubs.Publishers)
            {
                Console.WriteLine(publisher);
            }
        }
    }
}
