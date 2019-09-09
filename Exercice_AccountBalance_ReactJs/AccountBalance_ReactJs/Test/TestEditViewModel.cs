using Reactjs_Account;
using System;
using System.Threading;
using Xunit;

namespace Test
{
    public class TestEditViewModel 
    {
        private EditViewModel vm;

        public TestEditViewModel()
        {
            vm = new EditViewModel(new Service());
        }

        [Fact]
        public void Empty_inputs_should_throw_Exception()
        {
            Assert.Throws<ArgumentException>(() => vm.ButtonClicked(""));
        }


        [Fact]
        public void Invalid_inputs_should_throw_Exception()
        {
            vm.daily = "15";
            vm.over = "dqsdqs";
            Assert.Throws<ArgumentException>(() => vm.ButtonClicked(""));
            vm.daily = "qdqs";
            vm.over = "15";
            Assert.Throws<ArgumentException>(() => vm.ButtonClicked(""));
        }

        [Fact]
        public void valid_daily_should_work()
        {
            vm.daily = "100";
            vm.ButtonClicked("7f5d0507-501d-44d0-b0e4-616173d70bd2");
            Thread.Sleep(1000);
            Assert.Equal(100,vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("7f5d0507-501d-44d0-b0e4-616173d70bd2")).wiretransfertlimit );
        }

        [Fact]
        public void valid_over_should_work()
        {
            vm.over = "100";
            vm.ButtonClicked("7f5d0507-501d-44d0-b0e4-616173d70bd2");
            Thread.Sleep(1000);
            Assert.Equal(100, vm._accountService._readModel.list.Find(x => x.Id == Guid.Parse("7f5d0507-501d-44d0-b0e4-616173d70bd2")).overdraftlimit);
        }
    }
}
