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
            // testing save events
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Mohamed", 500, 200, 1000);
            acc.DeposeCash(200);
            acc.DeposeCheque(100);
            acc.WireTransfer(Guid.NewGuid(),300);
            acc.DeposeCash(20);
            acc.WithdrawCash(200);
            acc.WithdrawCash(200);

            var repo = new Repository<AccountBalance>();
            repo.SaveEvents(acc);

            Console.Read();
            
        }
    }
}
