using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Application.Common.Services;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Common.Services
{
    [TestFixture]
    public class RandomGeneratorServiceTests
    {
        private RandomGeneratorService _randomGenerator;

        [SetUp]
        public void Setup()
        {
            _randomGenerator = new RandomGeneratorService();
        }

        [Test]
        public void WhenGenerateRandomPositiveNumberIsCalled_GivenAnUpperbound_ShouldBeAPositiveNumberLessOrEqualToUpperbound()
        {
            const int upperbound = 10;

            for (var i = 0; i < upperbound; i++)
            {

                var result = _randomGenerator.GenerateRandomPositiveNumber(upperbound);
                result.ShouldBeInRange(1,upperbound);
            }
        }
    }
}
