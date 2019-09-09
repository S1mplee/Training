using Reactjs_Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Test
{
    public class TestAccountViewModel
    {
        private AccountViewModel vm;

        public TestAccountViewModel()
        {
            vm = new AccountViewModel(new Service());
        }

        [Fact]
        public void invalid_balance_throw_excpetion()
        {
            vm.value = "";

            Assert.Throws<ArgumentException>(() => vm.id("test"));
            Assert.Throws<ArgumentException>(() => vm.accountid("test"));
            Assert.Throws<ArgumentException>(() => vm.Tid("test"));

            vm.value = "sqddqsd";

            Assert.Throws<ArgumentException>(() => vm.id("test"));
            Assert.Throws<ArgumentException>(() => vm.accountid("test"));
            Assert.Throws<ArgumentException>(() => vm.Tid("test"));


        }

        [Fact]
        public void invalid_ID_throw_excpetion()
        {
            vm.value = "165";

            Assert.Throws<ArgumentException>(() => vm.id(""));
            Assert.Throws<ArgumentException>(() => vm.accountid(""));
            Assert.Throws<ArgumentException>(() => vm.Tid(""));

        }

        [Fact]
        public void CashDepose_ShouldWork()
        {
            vm.value = "100";
            var val1 = vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("7f5d0507-501d-44d0-b0e4-616173d70bd2")).cash;

            vm.id("7f5d0507-501d-44d0-b0e4-616173d70bd2");

            Thread.Sleep(1000);
            var val2 = vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("7f5d0507-501d-44d0-b0e4-616173d70bd2")).cash;

            Assert.Equal(val2, val1 + 100);

        }

        [Fact]
        public void WithDraw_ShouldWork()
        {
            vm.value = "100";
            var val1 = vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("7f5d0507-501d-44d0-b0e4-616173d70bd2")).cash;

            vm.accountid("7f5d0507-501d-44d0-b0e4-616173d70bd2");

            Thread.Sleep(1000);
            var val2 = vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("7f5d0507-501d-44d0-b0e4-616173d70bd2")).cash;

            Assert.Equal(val2, val1 - 100);

        }

        [Fact]
        public void Create_ShouldWork()
        {
            vm.name = "Mohamed";
            var g = Guid.NewGuid();
            vm.clicked(true);
            Thread.Sleep(1000);
            var name = vm._accountService._readModel.list[vm._accountService._readModel.list.Count-1].HolderName;

            Assert.Equal("Mohamed", name);

        }

        [Fact]
        public void TransferCash_ShouldWork()
        {
            vm.value = "100";
            var val1 = vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("6855ab8b-3b41-4048-a0c3-824f64ec075f")).cash;
            vm.Tid("6855ab8b-3b41-4048-a0c3-824f64ec075f");
            Thread.Sleep(1000);
            var val2 = vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("6855ab8b-3b41-4048-a0c3-824f64ec075f")).cash;

            Assert.Equal(val1 - 100, val2);

        }

        [Fact]
        public void invalid_name_throw_excpetion()
        {
            vm.name = "";

            Assert.Throws<ArgumentException>(() => vm.clicked(true));
         

        }
    }
}
