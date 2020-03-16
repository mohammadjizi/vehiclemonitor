using MonitorWeb.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonitorWeb.ServiceClients
{
    public interface ICustomerApiClient
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomers();

        Task<IEnumerable<CustomerOwnershipViewModel>> GetCustomerOwnerships();
    }
}
