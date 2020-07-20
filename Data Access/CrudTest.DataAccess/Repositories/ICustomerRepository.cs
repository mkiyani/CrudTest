using CrudTest.Application.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudTest.DataAccess
{
    public interface ICustomerRepository
    {
        Task<int> Add(Customers Customers);
        Task<int> Edit(Customers Customers);
        Task<Customers> Get(int id);
        Task<List<Customers>> GetList();
        Task<int> Delete(int id);
        bool Exists(int id);
    }
}
