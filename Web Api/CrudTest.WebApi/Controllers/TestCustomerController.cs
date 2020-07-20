using System.Collections.Generic;
using CrudTest.DataAccess;
using Microsoft.AspNetCore.Mvc;
using CrudTest.Application.Core.Models;
using System.Linq;

namespace CrudTest.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestCustomerController : ControllerBase
    {
        private readonly List<Customers> _list;
        public TestCustomerController(List<Customers> mockList)
        {
            _list = mockList;
        }
        [Route("list")]
        public ActionResult<List<Customers>> List()
        {
            return _list;
        }
        [Route("Get")]
        [HttpGet("{Id}")]
        public ActionResult<Customers> Get(int? id)
        {
            if (id == null)
            {
                return new NotFoundObjectResult("id not found");
            }
            var singlePost = _list.SingleOrDefault(m => m.CustomerId == id);
            if (singlePost == null)
            {
                return new NotFoundObjectResult("Post Not found");
            }

            return singlePost;
        }
        [Route("add")]
        [HttpPost]
        public ActionResult Add(Customers post)
        {
            if (!ModelState.IsValid)
            {
                return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(ModelState);
            }
            _list.Add(post);

            return CreatedAtAction("Get", post);
        }
        [Route("remove")]
        [HttpDelete("{id}")]
        public ActionResult Remove(int? id)
        {
            var postToDelete = _list.SingleOrDefault(m => m.CustomerId == id);
            if (postToDelete == null)
            {
                return new NotFoundObjectResult(postToDelete);
            }
            _list.Remove(postToDelete);
            return new OkObjectResult(postToDelete.GetFullName() + " deleted");
        }

        [Route("update")]
        [HttpPut("{id}")]
        public ActionResult Update(int? id, Customers post)
        {
            if (id == null || post == null)
            {
                return new NotFoundObjectResult("One of Id or customer is missing");
            }
            var postToUpdate = _list.FirstOrDefault(m => m.CustomerId == id);
            _list.Remove(postToUpdate);
            if (postToUpdate == null)
            {
                return new NotFoundObjectResult("The Post with the id is missing");
            }
            postToUpdate.BankAccountNumber = post.BankAccountNumber;
            postToUpdate.FirstName = post.FirstName;
            postToUpdate.LastName = post.LastName;
            postToUpdate.Email = post.Email;
            postToUpdate.DateOfBirth = post.DateOfBirth;
            postToUpdate.DialCode = post.DialCode;
            postToUpdate.PhoneNumber = post.PhoneNumber;
            _list.Add(postToUpdate);
            return new OkObjectResult(postToUpdate);
        }
    }
}
