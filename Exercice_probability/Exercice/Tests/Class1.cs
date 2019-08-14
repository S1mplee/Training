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
            Assert.Throws<InvalidOperationException>(() => Probability.Create(num1));
        }

        [Theory]
        [InlineData(1,0.5)]
        [InlineData(0.5, 0.5)]
        [InlineData(0.25, 0.5)]
        [InlineData(0.75, 0.5)]
        public void And_ShouldWork(decimal num1,decimal num2)
        {
            var A = Probability.Create(num1);
            var B = Probability.Create(num2);
            var expected = Probability.Create(num1*num2);
            var C = B.And(A); // p4 should be equal to p3
             
            Assert.True(C.Equals(expected));
        }

        [Theory]
        [InlineData(1,0.5)]
        [InlineData(0.5, 0.5)]
        [InlineData(0.25, 0.5)]
        [InlineData(0.75, 0.5)]
        [InlineData(0.9, 0.9)]
        public void Or_ShouldWork(decimal num1, decimal num2)
        {
            var A = Probability.Create(num1);
            var B = Probability.Create(num2);
            var actual = A.Or(B);
            var expected = Probability.Create(num1 + num2 - num1*num2);

            Assert.True(expected.Equals(actual));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(0.5)]
        [InlineData(0.75)]
        [InlineData(0.25)]
        [InlineData(0.6)]
        public void Inverse_ShouldWork(decimal num1)
        {
            var expected = Probability.Create(1 - num1);
            var A = Probability.Create(num1);
            var actual = A.Inverse();

            Assert.True(expected.Equals(actual));

        }
    }
}
