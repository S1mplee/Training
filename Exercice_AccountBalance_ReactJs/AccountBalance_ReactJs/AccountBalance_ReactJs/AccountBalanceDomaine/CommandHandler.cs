using AccountBalance_ReactJs.AccountBalanceDomaine;
using ReactiveDomain.Foundation;
using ReactiveDomain.Messaging;
using ReactiveDomain.Messaging.Bus;
using System;
using TestAccountBalance;

namespace Account
{
    public class AccountCommandHandler  : 
       IHandleCommand<CreateAccount>, 
        IHandleCommand<DeposeCheque>,
        IHandleCommand<DeposeCash>, 
        IHandleCommand<WithDrawCash>,
       IHandleCommand<TransferCash>,
       IHandleCommand<SetDailyTransfertLimit>,
        IHandleCommand<SetOverdraftLimit>
    {
        private readonly IRepository _repo;
        public AccountCommandHandler(IRepository repo)
        {
            this._repo = repo;
        }




        public CommandResponse Handle(CreateAccount command)
        {

            if (_repo.TryGetById<AccountBalance>(command.Id, out AccountBalance acc) && acc != null)
            {
                throw new InvalidOperationException("Alreay exist !");
            }
            acc = new AccountBalance(command.Id, command.Holdername);
            _repo.Save(acc);
            return command.Succeed();
        }

        public CommandResponse Handle(SetDailyTransfertLimit command)
        {

            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not exist !");
            }
            acc.SetWireTransfertLimit(command.amount);
            _repo.Save(acc);

            return command.Succeed();
        }

        public CommandResponse Handle(SetOverdraftLimit command)
        {

            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not exist !");
            }
            acc.SetOverDraftLimit(command.amount);
            _repo.Save(acc);

            return command.Succeed();
        }

        public CommandResponse Handle(DeposeCash command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            acc.DeposeCash(command.amount);
            _repo.Save(acc);

            return command.Succeed();
        }

        public CommandResponse Handle(WithDrawCash command)
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

            return command.Succeed();
        }

        public CommandResponse Handle(TransferCash command)
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

            return command.Succeed();
        }

        public CommandResponse Handle(DeposeCheque command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.Id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            acc.DeposeCheque(command.amount, command.DepositDate);
            _repo.Save(acc);

            return command.Succeed();
        }

    }

}