using ReactiveDomain.Foundation;
using ReactiveDomain.Messaging;
using ReactiveDomain.Messaging.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAccountBalance;

namespace Account
{
    public class AccountCommandHandler : IHandleCommand<CreateAccount>, IHandleCommand<DeposeCheque>,
        IHandleCommand<DeposeCash>, IHandleCommand<WithDrawCash>, IHandleCommand<TransferCash>
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
                acc = new AccountBalance(command.Id, command.Holdername, command.cash, command.overdraft, command.wiretranferlimit);
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
            catch(Exception ex)
            {
                _repo.Save(acc);
                throw new ArgumentException("");
            }
            return command.Succeed();
        }

        public CommandResponse Handle(TransferCash command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.id, out AccountBalance acc) || !_repo.TryGetById<AccountBalance>(command.reciverId, out AccountBalance acc2))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            acc.WireTransfer(command.reciverId,command.amount);
            _repo.Save(acc);
            return command.Succeed();
        }

        public CommandResponse Handle(DeposeCheque command)
        {
            if (!_repo.TryGetById<AccountBalance>(command.Id, out AccountBalance acc))
            {
                throw new InvalidOperationException("Does Not Exist !");
            }
            acc.DeposeCheque(command.amount);
            _repo.Save(acc);
            return command.Succeed();
        }
    }

}
