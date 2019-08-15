using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DDD.DomainModel;
namespace Tests
{
    public class Class1
    {
        [Theory]
        [InlineData("",5000,20000,500)]
        [InlineData("Sami", -5000, 20000,600)]
        [InlineData("Sami", 5000, -20000,355)]
        [InlineData("Sami", 5000, -20000, -355)]
        public void Create_WithInvalidInputShouldThrowException(string n,decimal d,decimal d2,decimal d3)
        {
            var acc = new AccountBalance();
            Assert.Throws<ArgumentException>(() => acc.Create(Guid.NewGuid(), n, d, d2,d3));
        }

        [Theory]
        [InlineData("mohamed", 5000, 20000,236)]
        [InlineData("mohamed", 500, 2000,689)]
        [InlineData("mohamed", 5000.60, 20000.50,565)]
        public void Create_WithValidInputsShouldGenerateEvent(string n, decimal d, decimal d2,decimal d3)
        {
            var g = Guid.NewGuid();
            var acc = new AccountBalance();
            acc.Create(g, n, d, d2,d3);

            Assert.True(acc.events.Count > 0);
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(0)]
        [InlineData(-200.56)]
        public void DeposeCheque_WithInvalidAmountShouldThrowException(decimal d)
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Mohamed", 500, 200, 1000);
            Assert.Throws<ArgumentException>(() => acc.DeposeCheque(d));
        }

        [Theory]
        [InlineData(652)]
        [InlineData(750)]
        [InlineData(1000)]
        public void DeposeCheque_AccountUnblocked_WithValidAmount_ShouldGenerateOneEvent(decimal d)
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Mohamed", 500, 200, 1000); // event1
            acc.DeposeCheque(d); // event 2

            Assert.True(acc.events.Count == 2);
        }

        [Theory]
        [InlineData(800)]
        [InlineData(750)]
        [InlineData(1000)]
        [InlineData(2000)]
        public void DeposeCheque_AccountBlocked_WithValidAmount_ShouldUnBlockAccount(decimal d)
        {
            var acc = new AccountBalance();
            var g = Guid.NewGuid();
            acc.Create(g, "Mohamed", 500, 200, -1000); // event1
            acc.DeposeCheque(d);

            Assert.True(acc.events.Count == 3);
        }
    }
}
