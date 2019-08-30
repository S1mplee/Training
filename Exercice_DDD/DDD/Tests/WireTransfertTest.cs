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
    public class WireTransfertTest
    {
        [Theory]
        [InlineData(-200)]
        [InlineData(0)]
        [InlineData(-200.56)]
        public void TransfertMoney_WithInvalidAmountShouldThrowException(decimal amount)
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Mohamed");
            Assert.Throws<ArgumentException>(() => acc.WireTransfer(amount));
        }

        [Fact]
        public async Task TransfertCash_WithValidAmount_ShouldGenerateOneEvent()
        {
            var sc = new ScenarioRunner();

            var g = Guid.NewGuid();
            var date = DateTime.Now;

            var cmd = new CreateAccount(g, "Ahmed");
            var cmd2 = new SetDailyTransfertLimit(g, 100);
            var cmd3 = new TransferCash(g, 80);

            var evt1 = new AccountCreated(g, "Ahmed");
            var evt2 = new DailyWireTransfertLimitSet(g, 100);
            var evt3 = new CashTransfered(g, 80);


            await sc.Run(def => def.Given()
            .When(new List<Command> { cmd, cmd2 , cmd3 })
            .Then(evt1, evt2 , evt3));
        }

        [Fact]
        public async Task TransfertCash_WithInValidAmount_BlockAccountt()
        {
            var sc = new ScenarioRunner();

            var g = Guid.NewGuid();
            var date = DateTime.Now;

            var cmd = new CreateAccount(g, "Ahmed");
            var cmd2 = new SetDailyTransfertLimit(g, 100);
            var cmd3 = new TransferCash(g, 200);

            var evt1 = new AccountCreated(g, "Ahmed");
            var evt2 = new DailyWireTransfertLimitSet(g, 100);
            var evt3 = new AccountBlocked(g);


            await sc.Run(def => def.Given()
            .When(new List<Command> { cmd, cmd2, cmd3 })
            .Then(evt1, evt2, evt3));
        }
    }
}
