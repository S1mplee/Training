using Account;
using Account.Commands;
using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAccountBalance;
using Xunit;
using Xunit.ScenarioReporting;

namespace Test
{
    public class TestDeposeCheque
    {
        [Theory]
        [InlineData(-200)]
        [InlineData(0)]
        [InlineData(-200.56)]
        public void DeposeCheque_WithInvalidAmountShouldThrowException(decimal amount)
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => acc.DeposeCheque(amount,DateTime.Now));
        }

        [Theory]
        [InlineData(652)]
        [InlineData(750)]
        [InlineData(1000)]
        public async Task DeposeCheque_AccountUnblocked_WithValidAmount_ShouldGenerateOneEvent(decimal amount)
        {
            var sc = new ScenarioRunner();

            var g = Guid.NewGuid();
            var date = DateTime.Now;
            var ac = new AccountBalance();
            var cmd = new CreateAccount(g, "Ahmed");
            var cmd2 = new DeposeCheque(g, amount, date);

            var evt1 = new AccountCreated(g, "Ahmed");
            var evt2 = new ChequeDeposed(g, amount, ac.GetReleaseDate(date));


            await sc.Run(def => def.Given()
            .When(new List<object> { cmd, cmd2 })
            .Then(evt1, evt2));
        }

        [Fact]
        public async Task DeposeCheque_AccountBlocked_WithValidAmount_ShouldUnBlockAccount()
        {
            var sc = new ScenarioRunner();

            var g = Guid.NewGuid();
            var date = DateTime.Now;
            var ac = new AccountBalance();

            var cmd = new CreateAccount(g, "Ahmed");
            var cmd2 = new DeposeCheque(g, 100, date); // command
            var cmd3 = new SetOverdraftLimit(g, 100);
            var cmd4 = new WithDrawCash(g, 400);
            var cmd5 = new DeposeCheque(g, 100,date);

            var evt1 = new AccountCreated(g, "Ahmed");
            var evt2 = new ChequeDeposed(g, 100, ac.GetReleaseDate(date));
            var evt3 = new OverDraftlimitSet(g, 100);
            var evt5 = new AccountBlocked(g);  // expected event
            var evt6 = new AccountUnblocked(g);

            await sc.Run(def => def.Given()
           .When(new List<object> { cmd, cmd2, cmd3, cmd4 , cmd5})
           .Then(evt1, evt2, evt3, evt5, evt2,evt6));
        }
    }
}
    /*
    public class TestDeposeCheque : TestService
    {
        public TestDeposeCheque() : base()
        {

        }
        [Fact]
        public void Can_Deposit_Cheque()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel", 1000, 200, 200);
            this.Command.Handle(cmd);
            var cmd2 = new DeposeCheque(g, 100);
            Assert.IsType<Success>(this.Command.Handle(cmd2));
        }

        [Fact]
        public void can_unblock_Account()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel", 1000, 200, 200);
            this.Command.Handle(cmd);
            var cmd2 = new WithDrawCash(g, 2000);
            this.Command.Handle(cmd2);
            var cmd3 = new DeposeCheque(g, 2000);

            Assert.IsType<Success>(this.Command.Handle(cmd3));
            Assert.True(this._readModel.list.Find(x => x.Id == g).blocked);
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(-100)]
        [InlineData(-2000)]
        [InlineData(-986)]
        public void can_not_deposit_Cheque_with_negative_amount(decimal amount)
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel", 1000, 200, 200);
            this.Command.Handle(cmd);
            var cmd2 = new DeposeCash(g, amount);
            Assert.Throws<ArgumentException>(() => this.Command.Handle(cmd2));
        }

        [Fact]
        public void can_not_depose_Cheque_into_notvalid_account()
        {
            var cmd = new DeposeCheque(Guid.NewGuid(), 100);
            Assert.Throws<InvalidOperationException>(() => this.Command.Handle(cmd));

        }
    }
    */

