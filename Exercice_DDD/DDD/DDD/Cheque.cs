using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD
{
    public class Cheque
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool Checked { get; set; }
        public Cheque(decimal amount,DateTime date)
        {
            this.Amount = amount;
            this.Date = date;
            this.Checked = false;
        }
    }
}
