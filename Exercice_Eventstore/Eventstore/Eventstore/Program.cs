
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Eventstore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write the product Informations :");
            var service = new SaleService();
            Console.WriteLine("Write product name :");
            var product = Console.ReadLine();
            Console.WriteLine("Write product qts :");
            var qts = Console.ReadLine();
            Console.WriteLine("Write product price :");
            var price = Console.ReadLine();
            service.WriteEvent(product, int.Parse(qts), decimal.Parse(price));

            


        }
    }
}
