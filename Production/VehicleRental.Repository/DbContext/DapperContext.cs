using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRental.Repository.DbContext
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _config;
        public DapperContext(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
            => new NpgsqlConnection(_config.GetConnectionString("VehicleRentalConnectionString"));
    }
}
