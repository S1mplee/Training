using DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yes
{
    class Program
    {
        static void Main(string[] args)
        {
            EventStoreMock ev = new EventStoreMock();
            CommandHandler c = new CommandHandler(ev);
            var g = Guid.NewGuid();
            c.Handle(new CreateAccount(g, "Mohamed"));
            Console.WriteLine(ev.dict[g].GetType());
            Console.WriteLine(ev.dict.Count);
            Console.Read();
        }
    }
}
