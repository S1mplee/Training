using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.DomainService;
using DDD.DomainModel;
namespace DDD
{
    public class CommandHandler
    {
        private readonly IRepository<AccountBalance> _repo;
        public CommandHandler()
        {
            this._repo = new Repository<AccountBalance>();
        }

        public void Handle(AccountCreate cmd)
        {
            var acc = new AccountBalance();
            acc.Create(cmd.Id, cmd._holderName, cmd._overdraftLimit, cmd._wireTransertLimit, cmd._cash);
            _repo.SaveEvents(acc);

        }

        public void Handle(ChequeDepose cmd)
        {
            AccountBalance acc = (AccountBalance) _repo.GetbyID(cmd.accountId.ToString());
            acc.DeposeCheque(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(CashDepose cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.accountId.ToString());
            acc.DeposeCash(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(CashWithdraw cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.accountId.ToString());
            acc.WithdrawCash(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(CashTransfer cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.accountId.ToString());
            acc.WireTransfer(cmd.receiverId, cmd.Amount);
            _repo.SaveEvents(acc);
        }
    }
}
