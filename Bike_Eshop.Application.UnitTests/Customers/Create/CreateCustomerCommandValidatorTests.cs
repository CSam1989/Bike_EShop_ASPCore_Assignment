using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bike_EShop.Application.Customers.Commands.Create;
using NUnit.Framework;
using Shouldly;

namespace Bike_Eshop.Application.UnitTests.Customers.Create
{
    public class CreateCustomerCommandValidatorTests : CommandTestBase
    {
        private CreateCustomerCommandValidator _validator;
        private const string Firstname = "Sam";
        private const string Name = "Ceustermans";
        private const string UserId = "123-456-789";

        [SetUp]
        public void Setup()
        {
            _validator = new CreateCustomerCommandValidator(Context);
        }

        [TestCase(49, true)]
        [TestCase(50, true)]
        [TestCase(51, false)]
        public void IsValid_ShouldBeExpectedResult_WhenFirstnameLengthIsGiven(int length, bool expected)
        {
            var firstName = new string('a', length);

            var command = new CreateCustomerCommand
            {
                FirstName = firstName,
                Name = Name,
                UserId = UserId
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [TestCase(49, true)]
        [TestCase(50, true)]
        [TestCase(51, false)]
        public void IsValid_ShouldBeExpectedResult_WhenNameLengthIsGiven(int length, bool expected)
        {
            var name = new string('a', length);

            var command = new CreateCustomerCommand
            {
                FirstName = Firstname,
                Name = name,
                UserId = UserId
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(expected);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsValid_ShouldBeFalse_WhenNameIsEmpty(string name)
        {
            var command = new CreateCustomerCommand
            {
                FirstName = Firstname,
                Name = name,
                UserId = UserId
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }

       [Test]
        public void IsValid_ShouldBeFalse_WhenFirstnameAndNameIsNotUnique()
        {
            var customer = Context.Customers.First();

            var command = new CreateCustomerCommand
            {
                FirstName = customer.FirstName,
                Name = customer.Name,
                UserId = UserId
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }

        //Deze wel nodig?? is net hetzelfde als de command test
        [Test]
        public void IsValid_ShouldBeTrue_WhenFirstnameAndNameIsUnique()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = Firstname,
                Name = Name,
                UserId = UserId
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(true);
        }
    }
}
