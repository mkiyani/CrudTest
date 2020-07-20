using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudTest.Application.Core.Models;
using CrudTest.DataAccess;

namespace CrudTest.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _repository;
        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetList());
            //return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Customers = await _repository.Get(id.Value);
            if (Customers == null)
            {
                return NotFound();
            }

            return View(Customers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customers Customers)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(Customers);
                return RedirectToAction(nameof(Index));
            }
            return View(Customers);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Customers = await _repository.Get(id.Value);
            if (Customers == null)
            {
                return NotFound();
            }
            return View(Customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customers Customers)
        {
            if (id != Customers.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Edit(Customers);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(Customers.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Customers);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Customers = await _repository.Get(id.Value);
            if (Customers == null)
            {
                return NotFound();
            }

            return View(Customers);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
