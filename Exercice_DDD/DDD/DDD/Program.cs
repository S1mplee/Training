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
            /*
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Ali", 500, 200, 2000);
            acc.DeposeCash(200);
            acc.DeposeCheque(100);
            acc.WireTransfer(Guid.NewGuid(),300);
            acc.DeposeCash(20);
            acc.WithdrawCash(200);
            acc.WithdrawCash(200);
            var repo = new Repository<AccountBalance>();
            repo.SaveEvents(acc);
            */
         
            /*
            var repo = new Repository<AccountBalance>();
            var g = new Guid("bb4ae9d7-a6ab-48e2-80ae-6d571a1423cf");
            var acc = repo.GetbyID(g.ToString());
            var ct = (AccountBalance)acc;
            Console.WriteLine(ct.ToString());
            */
            /*
            var cmdHandler = new CommandHandler();
            var cmd = new CashDepose(new Guid("bb4ae9d7-a6ab-48e2-80ae-6d571a1423cf"),20);
            cmdHandler.Handle(cmd);
            */
            Console.Read();
            
        }
    }
}
