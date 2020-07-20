using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Moq;
using System;
using System.Linq;
using CrudTest.Application.Core.Models;
using CrudTest.WebApi.Controllers;

namespace CrudTest.TDD.Test
{
    public class CustomerService_Tests
    {
        private readonly TestCustomerController _customerController;
        private readonly Mock<List<Customers>> _mockList;
        public CustomerService_Tests()
        {
            _mockList = new Mock<List<Customers>>();
            _customerController = new TestCustomerController(_mockList.Object);
        }

        [Fact]
        public void GetTest_ReturnsListofcustomers()
        {
            //arrange
            var mockcustomers = new List<Customers> {
                new Customers{FirstName = "Tdd One",LastName="test",DateOfBirth=DateTime.Now,DialCode=98,PhoneNumber=9120388600,Email="test@s.com",CustomerId=100},
                new Customers{FirstName = "Tdd and Bdd",LastName="test",DateOfBirth=DateTime.Now,DialCode=98,PhoneNumber=9120388600,Email="test2@s.com",CustomerId=200}
            };

            _mockList.Object.AddRange(mockcustomers);

            //act
            var result = _customerController.List();

            //assert
            var model = Assert.IsAssignableFrom<ActionResult<List<Customers>>>(result);
            Assert.Equal(2, model.Value.Count);
        }

        [Fact]
        public void GetTest_ReturnsNotFound_WhenIdNotProvided()
        {
            //act 
            var result = _customerController.Get(null);
            //asert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetTest_ReturnsNotFound_WhencustomerDoesNotExit()
        {
            //arrange
            var customer = new Customers() { CustomerId = 1254 };

            _mockList.Object.SingleOrDefault(m => m.CustomerId == customer.CustomerId);

            //act
            var result = _customerController.Get(customer.CustomerId);

            //assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetTest_ReturnsSinglecustomer_WhencustomerExist()
        {
            //arrange 
            var singleMockcustomer = new Customers()
            {
                CustomerId = 1000,
                FirstName = "new TDD",
                LastName = "test",
                Email = "m@test.com",
                BankAccountNumber = 1245,
                DateOfBirth = DateTime.Now,
                DialCode = 98,
                PhoneNumber = 9120388600
            };

            _mockList.Object.Add(singleMockcustomer);

            //act 
            var result = _customerController.Get(singleMockcustomer.CustomerId);

            //assert 
            var model = Assert.IsType<ActionResult<Customers>>(result);
            Assert.Equal(singleMockcustomer, model.Value);
        }

        [Fact]
        public void AddTest_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            //arrange 
            var titleMissing = new Customers() { CustomerId = 2000, FirstName = "test TDD" };

            _customerController.ModelState.AddModelError("Last Name", "Last Name field is required");

            //act 
            var result = _customerController.Add(titleMissing);

            //assert 
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddTest_ReturnsCreatedResponse_WhenValidObjectPassed()
        {
            //arrange
            var mockcustomer = new Customers { FirstName = "Tdd One", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test@s.com" };

            //act 
            mockcustomer.CustomerId = 3000;
            var result = _customerController.Add(mockcustomer);

            //assert
            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
        }

        [Fact]
        public void AddTest_ReturnsResponseHasCreatedItem_WhenValidObjectPassed()
        {
            //arrange 
            var mockcustomer = new Customers { CustomerId = 500, FirstName = "Tdd One 1", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test4@s.com" };
            //act
            var result = _customerController.Add(mockcustomer) as CreatedAtActionResult;
            var item = result.Value as Customers;

            //assert 
            Assert.IsType<Customers>(item);
            Assert.Equal("Tdd One 1", item.FirstName);
        }

        [Fact]
        public void RemoveTest_ReturnsNotFound_WhenidNotExisting()
        {
            //arrange
            var notExistingid = 5000;

            //act
            var result = _customerController.Remove(notExistingid);

            //assert 
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void RemoveTest_ReturnsOkResult_WhenidIsExisting()
        {
            //arrange 
            var mockcustomer = new Customers { CustomerId = 1500, FirstName = "Tdd One 3", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test4@s.com" };
            _mockList.Object.Add(mockcustomer);


            //act
            var result = _customerController.Remove(mockcustomer.CustomerId);

            //assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public void RemoveTest_RemovesOneItem_WhenidIsExisting()
        {
            var mockcustomer = new List<Customers>()
            {
               new Customers{FirstName = "Tdd 1",LastName="test",DateOfBirth=DateTime.Now,DialCode=98,PhoneNumber=9120388600,Email="test@s.com",CustomerId=2100},
                new Customers{FirstName = "Tdd 2",LastName="test",DateOfBirth=DateTime.Now,DialCode=98,PhoneNumber=9120388600,Email="test2@s.com",CustomerId=2200},
                new Customers{FirstName = "Tdd 3",LastName="test",DateOfBirth=DateTime.Now,DialCode=98,PhoneNumber=9120388600,Email="test2@s.com",CustomerId=2200}
            };
            _mockList.Object.AddRange(mockcustomer);

            //act 
            _customerController.Remove(2100);

            //assert
            Assert.Equal(2, _customerController.List().Value.Count());
        }

        [Fact]
        public void UpdateTest_ReturnsNull_WhenIdAndcustomerAreNull()
        {
            //act
            var result = _customerController.Update(null, null);

            //assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UpdateTest_ReturnsNull_WhenIdIsNotNullAndcustomerIsNull()
        {
            //arrange 
            var mockcustomerId = 3500;

            //act 
            var result = _customerController.Update(mockcustomerId, null);

            //assert 
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UpdateTest_ReturnsNull_WhenIdIsNullAndcustomerIsNotNull()
        {
            //arrange 
            var mockcustomer = new Customers { CustomerId = 1500, FirstName = "Tdd One 5", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test4@s.com" };

            //act
            var result = _customerController.Update(null, mockcustomer);

            //assert 
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UpdateTest_ReturnNotFoundResult_WhenIdNotExisting()
        {
            //arrange 
            var mockcustomer = new Customers { CustomerId = 35000, FirstName = "Tdd One 6", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test4@s.com" };

            //act
            var result = _customerController.Update(mockcustomer.CustomerId, mockcustomer);

            //assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UpdateTest_ReturnsOkResult_WhenIdIsPresent()
        {
            //arrange 
            var mockcustomer = new Customers { CustomerId = 45000, FirstName = "Tdd One 7", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test4@s.com" };

            _mockList.Object.Add(mockcustomer);

            //act
            var result = _customerController.Update(mockcustomer.CustomerId, mockcustomer);

            //assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateTest_ReturnsNewItemAfterUpdate_WhenIdIsPresent()
        {
            //arrange 
            var mockcustomer = new Customers { CustomerId = 5000, FirstName = "Tdd One 8", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test4@s.com" };

            _mockList.Object.Add(mockcustomer);

            var mockcustomerToUpdate = new Customers { CustomerId = 6000, FirstName = "Tdd One 8", LastName = "test", DateOfBirth = DateTime.Now, DialCode = 98, PhoneNumber = 9120388600, Email = "test4@s.com" };

            //act
            var result = _customerController.Update(mockcustomer.CustomerId, mockcustomerToUpdate);

            //assert
            var model = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(mockcustomerToUpdate.FirstName, _customerController.Get(mockcustomer.CustomerId).Value.FirstName);
        }
    }
}
