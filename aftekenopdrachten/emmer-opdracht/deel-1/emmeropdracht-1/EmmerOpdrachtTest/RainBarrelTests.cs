using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmerOpdrachtTest
{
    internal class RainBarrelTests
    {
        [Test]
        public void TestRainBarrel_CorrectCapacities()
        {
            // Arrange

            // Act

            // Assert
            Assert.DoesNotThrow(() => new RainBarrel(80));
            Assert.DoesNotThrow(() => new RainBarrel(100));
            Assert.DoesNotThrow(() => new RainBarrel(120));
        }

        [Test]
        public void TestRainBarrel_IncorrectCapacities()
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<FalseCapacityException>(() => new RainBarrel(50));
            Assert.Throws<FalseCapacityException>(() => new RainBarrel(150));
        }
    }
}
