using Account;
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
    public class TestCreateAccount
    {
        [Fact]
        public void Create_WithInvalidInputShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new AccountBalance(Guid.NewGuid(),""));
        }

        [Fact]
        public async Task Create_WithValidInputsShouldGenerateEvent()
        {
            var sc = new ScenarioRunner();
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "ahmed");
            var evt = new AccountCreated(g, "ahmed");

            await sc.Run(def => def.Given()
            .When(new List<object> { cmd })
            .Then(evt));
        }
    }
    /*
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
    */
}
