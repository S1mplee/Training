using Commands;
using DDD.DomainModel;
using DDD.DomainService;
using System;

namespace DDD
{
    
    public class CommandHandler : ICommandHandler<CreateAccount> ,
                                    ICommandHandler<DeposeCheque> ,
                                    ICommandHandler<DeposeCash> ,
                                    ICommandHandler<WithDrawCash> ,
                                    ICommandHandler<TransferCash>
    {
        private readonly IRepository<AccountBalance> _repo;
        public CommandHandler(IRepository<AccountBalance> repo)
        {
            this._repo = repo;
        }

        public void Handle(CreateAccount cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.AccountId);
            if (acc != null) throw new InvalidOperationException("Does Exist");
            var acc2 = new AccountBalance();
            acc2.Create(cmd.AccountId, cmd.HolderName);
            _repo.SaveEvents(acc2);

        }

        public void Handle(DeposeCheque cmd)
        {
            AccountBalance acc = (AccountBalance) _repo.GetbyID(cmd.AccountId);
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.DeposeCheque(cmd.Amount,cmd.DepositDate);
            _repo.SaveEvents(acc);
        }

        public void Handle(DeposeCash cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.AccountId);
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.DeposeCash(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(WithDrawCash cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.AccountId);
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            try
            {
                acc.WithdrawCash(cmd.Amount);
            }catch(Exception ex)
            {
                _repo.SaveEvents(acc);
            }
        }

        public void Handle(TransferCash cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.AccountId);
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.WireTransfer(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(SetOverdraftLimit cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.AccountId);
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.SetOverDraftLimit(cmd.Amount);
            _repo.SaveEvents(acc);
        }

        public void Handle(SetDailyTransfertLimit cmd)
        {
            AccountBalance acc = (AccountBalance)_repo.GetbyID(cmd.AccountId);
            if (acc == null) throw new InvalidOperationException("Does not Exist");
            acc.SetWireTransfertLimit(cmd.Amount);
            _repo.SaveEvents(acc);
        }


    }

    public interface ICommandHandler<T> where T : Command
    {
         void Handle(T cmd);
    }
}

