using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;
using VehicleRental.Repository.Interface;
using VehicleRental.Service.Interface;

namespace VehicleRental.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersList()
        {
            var result = await _customerRepository.GetCustomersList();

            return result;
        }
    }
}
