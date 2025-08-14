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

        public async Task<IEnumerable<CustomerEntity>> GetCustomersList(CustomerEntity customerEntity = default)
        {
            var query = new StringBuilder(@"
SELECT
id AS Id,
name AS CustomerName,
phone_number AS CustomerPhoneNumber
FROM customer
WHERE 1=1 ");
            if (!string.IsNullOrEmpty(customerEntity?.CustomerName))
            {
                query.AppendLine(" AND name = @CustomerName ");
            }
            if (!string.IsNullOrEmpty(customerEntity?.CustomerPhoneNumber))
            {
                query.AppendLine(" AND phone_number = @CustomerPhoneNumber ");
            }

            query.AppendLine(" ORDER BY id");

            var param = new
            {
                CustomerName = customerEntity?.CustomerName,
                CustomerPhoneNumber = customerEntity?.CustomerPhoneNumber,
            };

            using var connection = _context.GetConnection();
            var result = await connection.QueryAsync<CustomerEntity>(query.ToString(), param);

            return result ?? [];
        }
    }
}
