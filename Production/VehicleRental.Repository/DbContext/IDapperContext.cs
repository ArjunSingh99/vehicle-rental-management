using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRental.Repository.DbContext
{
    public interface IDapperContext
    {
        IDbConnection GetConnection();
    }
}
