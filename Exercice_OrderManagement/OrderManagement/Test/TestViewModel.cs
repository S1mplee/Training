using OrderManagement;
using System;
using System.Threading;
using TESTDICT;
using Xunit;

namespace Test
{
    public class TestViewModel
    {
        private ViewModel vm;

        public TestViewModel()
        {
            this.vm = new ViewModel(new Service());
        }

        [Fact]
        public void Empy_Fields_Throw_Exception()
        {
            vm.SimpleDropDownValue = "";
            vm.SimpleDropDownValue = "";
            vm.Price = "";
            vm.Qts = "";
            Assert.Throws<InvalidOperationException>(() => vm.ButtonClicked(true));
        }

        [Fact]
        public void Invalid_price_Throw_Exception()
        {
            vm.SimpleDropDownValue = "Buy";
            vm.SimpleDropDownValue = "Asset1";
            vm.Price = "-100";
            vm.Qts = "2";
            Assert.Throws<InvalidOperationException>(() => vm.ButtonClicked(true));
        }

        [Fact]
        public void Invalid_Qts_Throw_Exception()
        {
            vm.SimpleDropDownValue = "Buy";
            vm.SimpleDropDownValue = "Asset1";
            vm.Price = "100";
            vm.Qts = "-2";
            Assert.Throws<InvalidOperationException>(() => vm.ButtonClicked(true));
        }

        [Fact]
        public void valid_param_save_Asset()
        {
            Thread.Sleep(1000);
            var qts = vm.dict["Asset1"].Quantite;
            var expected = qts + 2;
            vm.SimpleDropDownValue = "Buy";
            vm.SimpleDropDownValue2 = "Asset1";
            vm.Price = "100";
            vm.Qts = "2";

            vm.ButtonClicked(true);

            Thread.Sleep(1000);
            var actual = vm.dict["Asset1"].Quantite;

            Assert.Equal(actual, expected);
        }
    }
}
