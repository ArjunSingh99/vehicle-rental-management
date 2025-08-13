using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;
using VehicleRental.Repository.DbContext;
using VehicleRental.Repository.Interface;

namespace VehicleRental.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDapperContext _context;

        public CustomerRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersList()
        {
            var query = new StringBuilder(@"
SELECT
id AS Id,
name AS CustomerName,
phone_number AS CustomerPhoneNumber
FROM customer
");

            using var connection = _context.GetConnection();
            var result = await connection.QueryAsync<CustomerEntity>(query.ToString());

            return result ?? [];
        }
    }
}
