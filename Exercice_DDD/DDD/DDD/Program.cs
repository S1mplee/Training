using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.DomainModel;
using DDD.DomainService;

namespace DDD
{
    class Program
    {
        static void Main(string[] args)
        {
            var acc = new AccountBalance();
            acc.Create(Guid.NewGuid(), "Mohamed", 500, 200, -1000);
            acc.DeposeCheque(800);

            foreach(var evt in acc.events)
            {
                Console.WriteLine(evt.GetType());
            }
            Console.Read();
        }
    }
}
