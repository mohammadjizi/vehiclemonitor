using CustomerApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
    }
}
