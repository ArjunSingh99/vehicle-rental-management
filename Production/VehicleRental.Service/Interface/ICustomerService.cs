using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;

namespace VehicleRental.Service.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerEntity>> GetCustomersList();
    }
}
