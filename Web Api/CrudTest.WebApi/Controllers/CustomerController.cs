using System.Collections.Generic;
using CrudTest.DataAccess;
using Microsoft.AspNetCore.Mvc;
using CrudTest.Application.Core.Models;

namespace CrudTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _repository = customerRepository;
        }
        [Route("list")]
        public async System.Threading.Tasks.Task<ActionResult<List<Customers>>> List()
        {
            return await _repository.GetList();
        }
        [Route("get")]
        [HttpGet("{Id}")]
        public async System.Threading.Tasks.Task<ActionResult<Customers>> Get(int? id)
        {
            if (id == null)
            {
                return new NotFoundObjectResult("id not found");
            }
            var customer = await _repository.Get(id.Value);
            if (customer == null)
            {
                return new NotFoundObjectResult("Customer Not found");
            }
            return customer;
        }
        [Route("add")]
        [HttpPost]
        public ActionResult Add(Customers Customer)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            _repository.Add(Customer);
            return CreatedAtAction("Get", Customer);
        }
        [Route("remove")]
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<ActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return new NotFoundObjectResult(id);
            }
            var CustomerToDelete = await _repository.Get(id.Value);
            if (CustomerToDelete == null)
            {
                return new NotFoundObjectResult(CustomerToDelete);
            }
            await _repository.Delete(id.Value);
            return new OkObjectResult(CustomerToDelete.GetFullName() + " deleted");
        }

        [Route("update")]
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<ActionResult> Update(int? id, Customers Customer)
        {
            if (id == null || Customer == null)
            {
                return new NotFoundObjectResult("One of Id or Customer is missing");
            }
            var CustomerToUpdate = await _repository.Get(id.Value);
            if (CustomerToUpdate == null)
            {
                return new NotFoundObjectResult("The Customer not found!");
            }
            await _repository.Edit(CustomerToUpdate);
            return new OkObjectResult(CustomerToUpdate);
        }
    }
}