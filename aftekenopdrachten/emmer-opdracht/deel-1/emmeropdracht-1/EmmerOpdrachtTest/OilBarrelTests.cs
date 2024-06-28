using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmmerOpdrachtTest
{
    internal class OilBarrelTests
    {
        [Test]
        public void TestOilBarrel_CorrectCapacity()
        {
            // Arrange

            // Act

            // Assert
            Assert.DoesNotThrow(() => new OilBarrel());
        }

        [Test]
        public void TestOilBarrel_Capacity()
        {
            // Arrange
            OilBarrel oilBarrel = new OilBarrel();
            // Act

            // Assert
            Assert.AreEqual(oilBarrel.Capacity, 159);
        }
    }
}
