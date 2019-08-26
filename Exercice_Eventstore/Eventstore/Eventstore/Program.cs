
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Eventstore
{
    class Program
    {

        static void Main(string[] args)
        {
            while(true)
            { 
            int number,number2;
            string product,qts;
            Console.WriteLine("Write the product Informations :");
            var service = new SaleService();
            service.ChargeProducts();
            service.show();
            do
            {
                Console.WriteLine("Write product Number :");
                product = Console.ReadLine();
            } while (!int.TryParse(product, out number) || number > service.list.Count || number < 1);

            do
            {
                Console.WriteLine("Write product qts :");
                qts = Console.ReadLine();
            } while (!int.TryParse(qts, out number2) || number2 <= 0);
            service.WriteEvent(service.list[number - 1].id, service.list[number - 1].ProductName, number2, service.list[number - 1].price);
                Console.WriteLine("Press Q to exist :");
                string p = Console.ReadLine();
                if (p == "q") break;
            }




        }
       
    }


    public class Product
    {
        public Guid id { get; set; }
        public string ProductName {get;set;}
        public decimal price { get; set; }
    }

   
}
