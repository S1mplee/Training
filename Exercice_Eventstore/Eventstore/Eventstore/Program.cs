using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eventstore
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var service = new SaleService();
            var menu = new Menu(service);
            menu.show();
        
        }
    }
}
