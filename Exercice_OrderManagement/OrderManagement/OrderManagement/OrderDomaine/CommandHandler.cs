using ReactiveDomain.Foundation;
using ReactiveDomain.Messaging;
using ReactiveDomain.Messaging.Bus;
using System;

namespace TESTDICT
{
    public class CommandHandler : IHandleCommand<CreateOrder>
    {
        private readonly IRepository _repo;
        public CommandHandler(IRepository repo)
        {
            this._repo = repo;
        }

        public CommandResponse Handle(CreateOrder command)
        {
            if (_repo.TryGetById<Order>(command.OrderId, out Order order) && order != null)
            {
                throw new InvalidOperationException("Alreay exist !");
            }
            order = new Order(command.OrderId, command.Action, command.Asset, command.Price, command.Quantite);
            _repo.Save(order);

            return command.Succeed();

        }
    }
}
