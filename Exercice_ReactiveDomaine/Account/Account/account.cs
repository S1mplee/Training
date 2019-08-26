using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account
{
    public class account
    {
        public Guid Id { get; set; }
        public string HolderName { get; set; }
        public decimal cash { get; set; }
        public decimal overdraftlimit { get; set; }
        public decimal wiretransfertlimit { get; set; }
        public bool blocked { get; set; }
    }
}
