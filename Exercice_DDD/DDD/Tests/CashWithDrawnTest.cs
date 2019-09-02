using Commands;
using DDD.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.ScenarioReporting;

namespace Tests
{
    public class WithDrawCashTest
    {
        [Fact]
        public void WithDraw_WithInvalid_Amount_Should_Throw_Exception()
        {
            var acc = new AccountBalance();
            Assert.Throws<ArgumentException>(() => acc.WithdrawCash(-200));
        }

        [Fact]
        public async Task WithDraw_Cash_ValidAmount_Should_WorkAsync()
        {
            var sc = new ScenarioRunner();

            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "Ahmed");
            var cmd2 = new DeposeCash(g, 100); // command
            var cmd3 = new SetOverdraftLimit(g, 100);
            var cmd4 = new WithDrawCash(g, 100);

            var evt1 = new AccountCreated(g, "Ahmed");
            var evt2 = new CashDeposed(g, 100);
            var evt3 = new OverDraftlimitSet(g, 100);
            var evt4 = new CashWithdrawn(g, 100);

            await sc.Run(def => def.Given()
        .When(new List<Command> { cmd, cmd2, cmd3, cmd4 })
        .Then(evt1, evt2, evt3, evt4));

        }
    }
}
