using DDD.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit.ScenarioReporting;
namespace Tests
{
    public class ScenarioRunner1 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private AccountCreated evt;
        public ScenarioRunner1()
        {
            value = new AccountBalance();
        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (CreateAccount)when;
            value.Create(cmd.Id, cmd._holderName, cmd._overdraftLimit, cmd._wireTransertLimit, cmd._cash);
            evt = (AccountCreated)value.events[0];
            return Task.CompletedTask;
        }
    }

    public class ScenarioRunner2 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private ChequeDeposed evt;

        public ScenarioRunner2()
        {
            value = new AccountBalance();

        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (DeposeCheque)when;
            value.Create(cmd.accountId, "Mohamed", 500, 200, 1000);
            value.DeposeCheque(cmd.Amount);
            evt = (ChequeDeposed)value.events[1];
            return Task.CompletedTask;
        }
    }

    public class ScenarioRunner3 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private AccountUnBlocked evt;

        public ScenarioRunner3()
        {
            value = new AccountBalance();

        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (DeposeCheque)when;
            value.Create(cmd.accountId, "Mohamed", 500, 200, -1000);
            value.DeposeCheque(cmd.Amount);
            evt = (AccountUnBlocked)value.events[2];
            return Task.CompletedTask;
        }
    }

    public class ScenarioRunner4 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private CashWithdrawn evt;

        public ScenarioRunner4()
        {
            value = new AccountBalance();

        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (WithDrawCash)when;
            value.Create(cmd.accountId, "Mohamed", 500, 200, 1000);
            value.WithdrawCash(cmd.Amount);
            evt = (CashWithdrawn)value.events[1];
            return Task.CompletedTask;
        }
    }

    public class ScenarioRunner5 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private CashTransfered evt;

        public ScenarioRunner5()
        {
            value = new AccountBalance();

        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (TransferCash)when;
            value.Create(cmd.accountId, "Mohamed", 500, 200, 1000);
            value.WireTransfer(cmd.accountId, cmd.Amount);
            evt = (CashTransfered)value.events[1];
            return Task.CompletedTask;
        }




    }

    ///

    public class ScenarioRunner6 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private AccountBlocked evt;

        public ScenarioRunner6()
        {
            value = new AccountBalance();

        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (WithDrawCash)when;
            value.Create(cmd.accountId, "Mohamed", 500, 200, 1000);
            value.WireTransfer(cmd.accountId, cmd.Amount);
            evt = (AccountBlocked)value.events[2];
            return Task.CompletedTask;
        }



    }
    public class ScenarioRunner7 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private CashTransfered evt;

        public ScenarioRunner7()
        {
            value = new AccountBalance();

        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (TransferCash)when;
            value.Create(cmd.accountId, "Mohamed", 500, 200, 1000);
            value.WireTransfer(cmd.receiverId, cmd.Amount);
            evt = (CashTransfered)value.events[1];
            return Task.CompletedTask;
        }


    }
    public class ScenarioRunner8 : ReflectionBasedScenarioRunner<object, object, object>
    {
        private AccountBalance value;
        private CashTransfered evt;
        private AccountBlocked evt2;

        public ScenarioRunner8()
        {
            value = new AccountBalance();

        }
        protected override Task<IReadOnlyList<object>> ActualResults()
        {

            return Task.FromResult((IReadOnlyList<object>)new[] { (object)evt, (object)evt2 });
        }

        protected override Task Given(IReadOnlyList<object> givens)
        {

            return Task.CompletedTask;
        }

        protected override Task When(object when)
        {
            var cmd = (TransferCash)when;
            value.Create(cmd.accountId, "Mohamed", 500, 200, 1000);
            value.WireTransfer(cmd.receiverId, cmd.Amount);
            evt = (CashTransfered)value.events[1];
            evt2 = (AccountBlocked)value.events[2];
            return Task.CompletedTask;
        }


        ///


    }
}
