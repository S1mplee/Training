using Account;
using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAccountBalance;
using Xunit;

namespace Test
{
    public class CreateAccountBalanceTest : TestService
    {
        public CreateAccountBalanceTest() : base()
        {

        }

        [Fact]
        public void can_Create_Account()
        {
            var cmd = new CreateAccount(Guid.NewGuid(), "Injez");
            Assert.IsType<Success>(this.Command.Handle(cmd));
        }

        [Fact]
        public void can_not_Create_Account_WithSame_ID()
        {
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "Mohamed");
            var cmd2 = new CreateAccount(g, "Mohamed");
            this.Command.Handle(cmd);
            Assert.Throws<InvalidOperationException>(() => this.Command.Handle(cmd2));
        }

        [Fact]
        public void can_not_Create_Account_WithInvalidInputs()
        {
            var cmd = new CreateAccount(Guid.NewGuid(), "");
            Assert.Throws<ArgumentException>(() => this.Command.Handle(cmd));
        }
    }
}
