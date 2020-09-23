using System;
using System.Collections.Generic;
using System.Text;
using Bike_EShop.Application.Products.Commands.Upsert;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Products.Commands.Upsert
{
    public class UpsertProductCommandValidatorTests: CommandTestBase
    {
        private UpsertProductCommandValidator _validator;
        private const string BikeRegistrationNumber = "ABC12345";
        private const string Name = "TestBike";
        private const decimal Price = 123;

        [SetUp]
        public void Setup()
        {
            _validator = new UpsertProductCommandValidator();
        }

        [TestCase(1, true)]
        [TestCase(50, true)]
        [TestCase(51, false)]
        public void IsValid_ShouldBeExpectedResult_WhenNameLengthIsGiven(int length, bool expected)
        {
            var name = new string('a', length);

            var command = new UpsertProductCommand
            {
                BikeRegistrationNumber = BikeRegistrationNumber,
                Name = name,
                Price = Price
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsValid_ShouldBeFalse_WhenNameIsEmpty(string name)
        {
            var command = new UpsertProductCommand
            {
                BikeRegistrationNumber = BikeRegistrationNumber,
                Name = name,
                Price = Price
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }

        [TestCase(1, true)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        public void IsValid_ShouldBeExpectedResult_WhenPriceIsGiven(decimal price, bool expected)
        {
            var command = new UpsertProductCommand
            {
                BikeRegistrationNumber = BikeRegistrationNumber,
                Name = Name,
                Price = price
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsValid_ShouldBeFalse_WhenBikeRegistrationNumberIsEmpty(string bikeRegistrationNumber)
        {
            var command = new UpsertProductCommand
            {
                BikeRegistrationNumber = bikeRegistrationNumber,
                Name = Name,
                Price = Price
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }

        [TestCase(7, false)]
        [TestCase(8, true)]
        [TestCase(9, false)]
        public void IsValid_ShouldBeExpectedResult_WhenBikeRegistrationNumberIsGiven(int length, bool expected)
        {
            var bikeRegistrationNumber = "ABC" + new string('a', length -3);

            var command = new UpsertProductCommand
            {
                BikeRegistrationNumber = bikeRegistrationNumber,
                Name = Name,
                Price = Price
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [TestCase("ABC", true)]
        [TestCase("DEF", false)]
        public void IsValid_ShouldBeExpectedResult_WhenBikeRegistrationNumberIsGiven(string startingLetters, bool expected)
        {
            var command = new UpsertProductCommand
            {
                BikeRegistrationNumber = startingLetters + "12345",
                Name = Name,
                Price = Price
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }
    }
}
