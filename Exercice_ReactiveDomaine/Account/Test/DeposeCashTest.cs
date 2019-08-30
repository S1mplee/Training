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
    public class DeposeCashTest
    {
        [Theory]
        [InlineData(-200)]
        [InlineData(0)]
        [InlineData(-200.56)]
        public void DeposeCash_WithInvalidAmountShouldThrowException(decimal amount)
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => acc.DeposeCash(amount));
        }

        [Theory]
        [InlineData(652)]
        [InlineData(750)]
        [InlineData(1000)]
        public async Task DeposeCash_AccountUnblocked_WithValidAmount_ShouldGenerateOneEvent(decimal amount)
        {
            var sc = new ScenarioRunner();

            var g = Guid.NewGuid();
            var date = DateTime.Now;
            var cmd = new CreateAccount(g, "Ahmed");
            var cmd2 = new DeposeCash(g, amount);
            var evt1 = new AccountCreated(g, "Ahmed");
            var evt2 = new CashDeposed(g, amount);


            await sc.Run(def => def.Given()
            .When(new List<object> { cmd, cmd2 })
            .Then(evt1, evt2));
        }

        [Fact]
        public async Task DeposeCash_AccountBlocked_WithValidAmount_ShouldUnBlockAccount()
        {
            var sc = new ScenarioRunner();

            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "Ahmed");
            var cmd2 = new DeposeCash(g, 100); // command
            var cmd3 = new SetOverdraftLimit(g, 100);
            var cmd4 = new WithDrawCash(g, 400);

            var evt1 = new AccountCreated(g, "Ahmed");
            var evt2 = new CashDeposed(g, 100);
            var evt3 = new OverDraftlimitSet(g, 100);
            var evt5 = new AccountBlocked(g);  // expected event

            await sc.Run(def => def.Given()
           .When(new List<object> { cmd, cmd2, cmd3, cmd4 })
           .Then(evt1, evt2, evt3, evt5));
        }
    }
    /*
    public class DeposeCashTest : TestService
    {
        public DeposeCashTest() : base()
        {

        }
        [Fact]
        public void Can_Deposit_Cash()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel");
            this.Command.Handle(cmd);
            var cmd2 = new DeposeCash(g, 100);
            Assert.IsType<Success>(this.Command.Handle(cmd2));
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(-100)]
        [InlineData(-2000)]
        [InlineData(-986)]
        public void can_not_deposit_cash_with_negative_amount(decimal amount)
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel");
            this.Command.Handle(cmd);
            var cmd2 = new DeposeCash(g, amount);
            Assert.Throws<ArgumentException>(() => this.Command.Handle(cmd2));
        }

        [Fact]
        public void can_not_depose_cash_into_notvalid_account()
        {
            var cmd = new DeposeCash(Guid.NewGuid(), 100);
            Assert.Throws<InvalidOperationException>(() => this.Command.Handle(cmd));
        }

        [Fact]
        public void can_unblock_Account()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel");
            this.Command.Handle(cmd);
            var cmd2 = new WithDrawCash(g, 2000);
            this.Command.Handle(cmd2);
            var cmd3 = new DeposeCash(g, 2000);

            Assert.IsType<Success>(this.Command.Handle(cmd3));
            Assert.True(this._readModel.list.Find(x => x.Id == g).blocked);
        }


        

    }
    */
}
