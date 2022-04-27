using Xunit;

namespace HeatCalculator.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(100, -20, 40, 13000)]
        [InlineData(200, 25, 100, 123000)]
        [InlineData(2000, -5, 5, 175000)]
        public void HeatTest(double m, double ti, double tf, double expect)
        {
            var heat = new Heat(m, ti, tf);
            Assert.Equal(expect, heat.Q);
            Assert.Equal(m, heat.M);
            Assert.Equal(ti, heat.Ti);
            Assert.Equal(tf, heat.Tf);
        }
    }
}