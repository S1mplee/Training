using Eventstore;
using System;
using Xunit;

namespace Tests
{
    
    public class SaleServiceTest
    {
      

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
            Assert.Throws<InvalidOperationException>(() => service.WriteEvent("Mouse", -2, 12.0m));
        }

        [Fact]
        public void GetProducts_ShouldWork()
        {
            var service = new SaleService();
            var list = service.GetProductsSold();
            Assert.NotNull(list);
        }

       

        [Fact]
        public void TotalSales_ShouldWork()
        {
            var service = new SaleService();
            var total = service.getTotal();
            Assert.True(total > -1);
        }
    }
    
}
