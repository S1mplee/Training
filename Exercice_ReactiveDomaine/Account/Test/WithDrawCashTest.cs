using Account;
using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    /*
    public class WithDrawCashTest : TestService
    {
        public WithDrawCashTest(): base()
        {

        }

        [Fact]
        public void can_withdraw_with_valid_account()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel");
            this.Command.Handle(cmd);
            var cmd2 = new WithDrawCash(g, 200);
            Assert.IsType<Success>(this.Command.Handle(cmd2));
        }

        [Fact]
        public void can_not_withdraw_with_Invalid_account()
        {
            var cmd = new WithDrawCash(Guid.NewGuid(), 200);
            Assert.Throws<InvalidOperationException>(() => this.Command.Handle(cmd));
        }

        [Fact]
        public void can_not_withdraw_with_Invalid_amount()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel");
            this.Command.Handle(cmd);
            var cmd2 = new WithDrawCash(g, -200);
            Assert.Throws<ArgumentException>(() => this.Command.Handle(cmd2));
        }

        [Fact]
        public void can_Withdraw_Block_Account()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "bilel");
            this.Command.Handle(cmd);
            var cmd2 = new WithDrawCash(g, 2000);
            Assert.Throws<ArgumentException>(() => this.Command.Handle(cmd2));
            Thread.Sleep(2000);
            Assert.True(this._readModel.list.Find(x => x.Id == g).blocked);
        }
    }
    */
}
