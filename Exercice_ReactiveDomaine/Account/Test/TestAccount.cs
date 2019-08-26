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
            var cmd = new CreateAccount(Guid.NewGuid(), "Injez", 1000, 500, 200);
            Assert.IsType<Success>(this.Command.Handle(cmd));
        }

        [Fact]
        public void can_not_Create_Account_WithSame_ID()
        {
            var cmd = new CreateAccount(Guid.Parse("98f9264b-abb7-444d-9802-ff8648c86210"), "Mohamed", 1000, 200, 300);
            Assert.Throws<InvalidOperationException>(() => this.Command.Handle(cmd));
        }

        [Fact]
        public void can_not_Create_Account_WithInvalidInputs()
        {
            var cmd = new CreateAccount(Guid.NewGuid(), "", 1000, 200, 300);
            Assert.Throws<ArgumentException>(() => this.Command.Handle(cmd));
        }
    }
}
