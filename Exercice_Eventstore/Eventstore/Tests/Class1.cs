using Eventstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class Class1
    {
        [Theory]
        [InlineData("Car",2,2.0)]
        [InlineData("mouse", 9, 3.0)]
        [InlineData("keyboard",15, 2.5)]
        [InlineData("banane", 8, 1.9)]
        public void Create_WithValidInputShouldReturnSaleAchievedEvent(string name,int qts,decimal price)
        {
            var g = Guid.NewGuid();
            var evt = new SaleAchieved(g, name, qts, price);
            var evt2 = Sale.Create(g, name, qts, price);
            Assert.True(evt.Equals(evt2));
        }

        [Theory]
        [InlineData("", 2, 2.0)]
        [InlineData( "car", -2, 2.0)]
        [InlineData( "car", 2, -2.0)]
        public void Create_WithInvalidInputsShouldThrowException(string name, int qts, decimal price)
        {
            Assert.Throws<ArgumentException>(() => Sale.Create(Guid.NewGuid(), name, qts, price));
        }

        [Fact]
        public void WriteEvent_WithValidInputsShouldReturnTrue()
        {
            var service = new SaleService();
            Assert.True(service.WriteEvent("Mouse",2,12.0m));
        }

        [Fact]
        public void WriteEvent_WithInvalidValidInputsShouldThrowException()
        {
            var service = new SaleService();
            Assert.Throws<ArgumentException>(() => service.WriteEvent("Mouse", -2, 12.0m));
        }
    }
}
