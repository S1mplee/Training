using AccountBalance_ReactJs.AccountBalanceDomaine;
using ReactiveDomain.Foundation;
using System;
using TestAccountBalance;

namespace Account
{
    public class AccountCommandHandler  : 
       HandleCommand<CreateAccount>, 
        HandleCommand<DeposeCheque>,
        HandleCommand<DeposeCash>, 
        HandleCommand<WithDrawCash>,
       HandleCommand<TransferCash>,
       HandleCommand<SetDailyTransfertLimit>,
        HandleCommand<SetOverdraftLimit>
    {
        private readonly IRepository _repo;
        public AccountCommandHandler(IRepository repo)
        {
            this._repo = repo;
        }




        public void Handle(CreateAccount command)
        {

            if (_repo.TryGetById<AccountBalance>(command.Id, out AccountBalance acc) && acc != null)
            {
                throw new InvalidOperationException("Alreay exist !");
            }
            acc = new AccountBalance(command.Id, command.Holdername);
            _repo.Save(acc);

        }

        public void Handle(SetDailyTransfertLimit command)
        {

            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not exist !");
            }
            acc.SetWireTransfertLimit(command.amount);
            _repo.Save(acc);


        }

        public void Handle(SetOverdraftLimit command)
        {

            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not exist !");
            }
            acc.SetOverDraftLimit(command.amount);
            _repo.Save(acc);


        }

        public void Handle(DeposeCash command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            acc.DeposeCash(command.amount);
            _repo.Save(acc);
        }

        public void Handle(WithDrawCash command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            try
            {
                acc.WithdrawCash(command.amount);
                _repo.Save(acc);
            }
            catch (Exception ex)
            {
                _repo.Save(acc);
            }
        }

        public void Handle(TransferCash command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            try
            {
                acc.WireTransfer(command.amount, command.TransferDate);
                _repo.Save(acc);
            }
            catch (Exception ex)
            {
                _repo.Save(acc);
            }

        }

        public void Handle(DeposeCheque command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.Id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            acc.DeposeCheque(command.amount, command.DepositDate);
            _repo.Save(acc);
        }
    }

}