using Director;
using Eventstore;
using inventory_manager;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    
    public class SaleServiceTest
    {
      

        [Fact]
        public void WriteEvent_WithValidInputsShouldReturnTrue()
        {
            var service = new SaleService();
            Assert.True(service.WriteEvent(Guid.NewGuid(),"Mouse",2,12.0m));
        }

        [Fact]
        public void WriteEvent_WithInvalidValidInputsShouldThrowException()
        {
            var service = new SaleService();
            Assert.Throws<InvalidOperationException>(() => service.WriteEvent(Guid.NewGuid(), "Mouse", -2, 12.0m));
        }

        [Fact]
        public void ChargeProducts_ShouldWork()
        {
            
            var service = new SaleService();
            service.ChargeProducts();
            Assert.NotNull(service.list);
            Assert.True(service.list.Count > 0);
        }

       

        [Fact]
        public void Read_Products_ShouldWork()
        {
            var store = new EventStoree();
            store.Connect();
            var service = new Inventoryservice(store);
            service.Read(store);
            Assert.True(service.products.Body.Count > 0);
        }

        [Fact]
        public void GetTotatl_ShouldWork()
        {
            var store = new EventStoree();
            store.Connect();
            var service = new DirectorService(store);
            List<SaleAchieved> list = new List<SaleAchieved>{ new SaleAchieved(Guid.NewGuid(),Guid.NewGuid(),"",1,10),
                new SaleAchieved(Guid.NewGuid(),Guid.NewGuid(),"",1,10)
            };
            var sum = service.TotalSales(list);
            decimal sum2 = 20;
            Assert.Equal(sum, sum2);

        }
    }
    
}
