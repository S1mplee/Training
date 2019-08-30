using System;

namespace TestAccountBalance
{
    public class Cheque
    {
        public decimal Amount;
        public DateTime Date;
        public bool Checked;

        public Cheque(decimal amount,DateTime date)
        {
            this.Amount = amount;
            this.Date = date;
            this.Checked = false;
        }
    }
}