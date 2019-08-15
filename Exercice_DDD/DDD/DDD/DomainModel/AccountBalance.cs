using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.DomainModel
{
    public class AccountBalance
    {
        private Guid Id;
        private string _holderName;
        private decimal _overdraftLimit;
        private decimal _wireTransertLimit;
        private bool _blocked;
    }
}
