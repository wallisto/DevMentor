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
            dynamic parser = new DynamicXmlParser(@"..\..\order.xml");
            Console.WriteLine(parser["orderId"]);
            Console.WriteLine(parser.customer.name);
            Console.WriteLine(parser.orderItem.product);
            Console.WriteLine(parser.orderItem.supplier);
            Console.WriteLine(parser.wtf.doesntexist);
        }
    }
}
