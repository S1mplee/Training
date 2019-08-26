using Account;
using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class DeposeCashTest : TestService
    {
        public DeposeCashTest() : base()
        {

        }
        [Fact]
        public void Can_Deposit_Cash()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel", 1000, 200, 200);
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
            var cmd = new CreateAccount(g, "bilel", 1000, 200, 200);
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
            var cmd = new CreateAccount(g, "bilel", 1000, 200, 200);
            this.Command.Handle(cmd);
            var cmd2 = new WithDrawCash(g, 2000);
            this.Command.Handle(cmd2);
            var cmd3 = new DeposeCash(g, 2000);

            Assert.IsType<Success>(this.Command.Handle(cmd3));
            Assert.True(this._readModel.list.Find(x => x.Id == g).blocked);
        }


        

    }
}
