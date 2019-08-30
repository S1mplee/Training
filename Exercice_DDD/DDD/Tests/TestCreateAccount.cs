using Commands;
using DDD.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.ScenarioReporting;

namespace Tests
{
    public class TestCreateAccount
    {
        [Fact]
        public void Create_WithInvalidInputShouldThrowException()
        {
            var acc = new AccountBalance();
            Assert.Throws<ArgumentException>(() => acc.Create(Guid.NewGuid(), ""));
        }

        [Fact]
        public async Task Create_WithValidInputsShouldGenerateEvent()
        {
            var sc = new ScenarioRunner();
            var g = Guid.NewGuid();
            var cmd = new CreateAccount(g, "ahmed");
            var evt = new AccountCreated(g, "ahmed");

            await sc.Run(def => def.Given()
            .When(new List<Command> { cmd })
            .Then(evt));
        }
    }
}
