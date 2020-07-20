using CrudTest.Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudTest.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DBContext _context;
        public CustomerRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Customers Customers)
        {

            if (_context.Customers.Any(e => e.Email == Customers.Email))
                throw new Exception("Duplicate Email!");
            if (_context.Customers.Any(e => e.FirstName == Customers.FirstName && e.LastName == Customers.LastName && e.DateOfBirth == Customers.DateOfBirth))
                throw new Exception("Duplicate Customer!");
            Customers.CreateDate = DateTime.UtcNow;
            _context.Add(Customers);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> Edit(Customers Customers)
        {
            if (_context.Customers.Any(e => e.Email == Customers.Email && e.CustomerId != Customers.CustomerId))
                throw new Exception("Duplicate Email!");
            if (_context.Customers.Any(e => e.CustomerId != Customers.CustomerId && e.FirstName == Customers.FirstName && e.LastName == Customers.LastName && e.DateOfBirth == Customers.DateOfBirth))
                throw new Exception("Duplicate Customer!");
            Customers.LastUpdateDate = DateTime.UtcNow;
            _context.Update(Customers);
            return await _context.SaveChangesAsync();
        }
        public async Task<Customers> Get(int id)
        {
            return await _context.Customers.FindAsync(id);
        }
        public bool Exists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
        public async Task<List<Customers>> GetList()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<int> Delete(int id)
        {
            var Customers = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(Customers);
            return await _context.SaveChangesAsync();
        }
    }
}
