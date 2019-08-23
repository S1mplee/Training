using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.DomainService;
using DDD.DomainModel;
namespace DDD
{
    public class CommandHandler : ICommandHandler<CreateAccount> , ICommandHandler<DeposeCheque> ,
        ICommandHandler<DeposeCash> , ICommandHandler<WithDrawCash> , ICommandHandler<TransferCash>
    {
        private readonly IRepository<AccountBalance> _repo;
        public CommandHandler()
        {
            this._repo = new Repository<AccountBalance>();
        }

        public void Handle(CreateAccount cmd)
        {
            var acc = new AccountBalance();
            acc.Create(cmd.Id, cmd._holderName, cmd._overdraftLimit, cmd._wireTransertLimit, cmd._cash);
            _repo.SaveEvents(acc);

        }

        public void Handle(DeposeCheque cmd)
        {
            AccountBalance acc = (AccountBalance) _repo.GetbyID(cmd.accountId.ToString());
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.DeposeCheque(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(DeposeCash cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.accountId.ToString());
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.DeposeCash(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(WithDrawCash cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.accountId.ToString());
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.WithdrawCash(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(TransferCash cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.accountId.ToString());
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.WireTransfer(cmd.receiverId, cmd.Amount);
            _repo.SaveEvents(acc);
        }

        
    }

    public interface ICommandHandler<T> where T : Command
    {
         void Handle(T cmd);
    }
}
