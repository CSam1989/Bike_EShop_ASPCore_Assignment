using Bike_EShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bike_EShop.Application.Common.Services
{
    public class RandomGeneratorService : IRandomGeneratorService
    {
        public int GenerateRandomPositiveNumber(int upperBound)
        {
            var random = new Random();
            return random.Next(1, upperBound + 1);
        }
    }
}
