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
            TitlePriceRise(0.20m);
        }

        private static void TitlePriceRise(decimal percentageRise)
        {
            using (var pubs = new Pubs())
            {
                foreach (Title title in pubs.Titles)
                {
                    title.Price *= 1 + percentageRise;
                }

                pubs.SaveChanges();
            }

            ListPublishersAndTitles();
        }

        private static void ListPublishersAndTitles()
        {
            using (var pubs = new Pubs())
            {
                pubs.Configuration.LazyLoadingEnabled = false;

                foreach (Publisher publisher in pubs.Publishers.Include(p => p.Titles))
                {
                    Console.WriteLine(publisher);
                    foreach (Title title in publisher.Titles)
                    {
                        Console.WriteLine("\t{0} @ {1:C}", title.Name, title.Price);
                    }
                }
            }
        }

        private static void ListPublishers()
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
