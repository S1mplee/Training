using Exercice;
using System;
using Xunit;

namespace Tests
{
    public class Test
    {

        [Theory]
        [InlineData(1.5)]
        [InlineData(3)]
        [InlineData(-1)]
        [InlineData(-0.5)]
        public void Create_InvalidInputShouldThrowException(decimal value)
        {
            Assert.Throws<InvalidOperationException>(() => Probability.Create(value));
        }

        [Theory]
        [InlineData(1,0.5)]
        [InlineData(0.5, 0.5)]
        [InlineData(0.25, 0.5)]
        [InlineData(0.75, 0.5)]
        public void And_ShouldWork(decimal value_A, decimal value_B)
        {
            var A = Probability.Create(value_A);
            var B = Probability.Create(value_B);
            var expected = Probability.Create(value_A * value_A);
            var C = B.And(A); // p4 should be equal to p3
             
            Assert.True(C.Equals(expected));
        }

        [Theory]
        [InlineData(1,0.5)]
        [InlineData(0.5, 0.5)]
        [InlineData(0.25, 0.5)]
        [InlineData(0.75, 0.5)]
        [InlineData(0.9, 0.9)]
        public void Or_ShouldWork(decimal value_A, decimal value_B)
        {
            var A = Probability.Create(value_A);
            var B = Probability.Create(value_B);
            var actual = A.Or(B);
            var expected = Probability.Create(value_A + value_B - value_A * value_B);

            Assert.True(expected.Equals(actual));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(0.5)]
        [InlineData(0.75)]
        [InlineData(0.25)]
        [InlineData(0.6)]
        public void Inverse_ShouldWork(decimal value)
        {
            var expected = Probability.Create(1 - value);
            var A = Probability.Create(value);
            var actual = A.Inverse();

            Assert.True(expected.Equals(actual));

        }
    }
}
