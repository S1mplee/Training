using Commands;
using DDD.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.ScenarioReporting;

namespace Tests
{
    public class SetDailyWireTransfertTest
    {
        [Fact]
        public void SetOverDraftLimit_WithInvalidAmountShouldThrowException()
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Mohamed");

            Assert.Throws<ArgumentException>(() => acc.SetWireTransfertLimit(g, -200));
        }

        [Fact]
        public async Task SetOVerDraftLimit_WithValidInputsShouldGenerateEventAsync()
        {
            var sc = new ScenarioRunner();
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "ahmed");
            var cmd2 = new SetDailyTransfertLimit(g, 1000);

            var evt1 = new AccountCreated(g, "ahmed");
            var evt2 = new DailyWireTransfertLimitSet(g, 1000);

            await sc.Run(def => def.Given()
          .When(new List<Command> { cmd, cmd2 })
          .Then(evt1, evt2));
        }
    }
}
