using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"..\..\order.xml";

            dynamic parser = new DynamicXmlParser(file);

            Console.WriteLine(parser["orderId"]);
            Console.WriteLine(parser.customer.name);
            Console.WriteLine(parser.orderItem.product);
            Console.WriteLine(parser.orderItem.supplier);
        }
    }
}
