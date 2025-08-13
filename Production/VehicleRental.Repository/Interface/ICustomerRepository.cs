using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;

namespace VehicleRental.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> GetCustomersList();
    }
}
