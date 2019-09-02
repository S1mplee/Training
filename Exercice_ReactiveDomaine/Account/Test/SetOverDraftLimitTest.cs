using Account;
using Account.Commands;
using ReactiveDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestAccountBalance;
using Xunit;
using Xunit.ScenarioReporting;

namespace Test
{
    public class SetOverDraftLimitTest
    {
        [Fact]
        public void SetOverDraftLimit_WithInvalidAmountShouldThrowException()
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() => acc.SetOverDraftLimit(-200));
        }

        [Fact]
        public async Task SetOVerDraftLimit_WithValidInputsShouldGenerateEventAsync()
        {
            var sc = new ScenarioRunner();
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "ahmed");
            var cmd2 = new SetOverdraftLimit(g, 1000);

            var evt1 = new AccountCreated(g, "ahmed");
            var evt2 = new OverDraftlimitSet(g, 1000);

            await sc.Run(def => def.Given()
          .When(new List<object> { cmd, cmd2 })
          .Then(evt1, evt2));
        }
    }
}
