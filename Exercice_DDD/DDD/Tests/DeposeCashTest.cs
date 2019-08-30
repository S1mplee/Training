using Commands;
using DDD.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.ScenarioReporting;

namespace Tests
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
            acc.Create(g, "Mohamed");
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
            .When(new List<Command> { cmd, cmd2 })
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
           .When(new List<Command> { cmd, cmd2, cmd3, cmd4 })
           .Then(evt1, evt2, evt3, evt5));
        }
    }
}
