using Exercice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Theory]
        [InlineData(0.2)]
        [InlineData(1)]
        [InlineData(0.75)]
        [InlineData(0.9)]
        public void Create_ValidinputShouldWork(decimal num1)
        {
            var expected = Probability.Create(num1);

            Assert.True(Probability.Compare(expected,num1));
        }

        [Theory]
        [InlineData(1.5)]
        [InlineData(3)]
        [InlineData(-1)]
        [InlineData(-0.5)]
        public void Create_InvalidInputShouldThrowException(decimal num1)
        {
            Assert.Throws<InvalidOperationException>(() => Probability.Create(-2));
        }
    }
}
