using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRental.Repository.DbContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        public IDbTransaction Transaction { get; set; }

        public UnitOfWork(IDapperContext context)
        {
            _connection = context.GetConnection();
        }

        public IDbConnection GetConnection()
            => _connection;

        public void BeginTransaction()
        {
            _connection.Open();
            Transaction = _connection.BeginTransaction();
        }

        public IDbTransaction GetCurrentTransaction()
            => Transaction;

        public void Commit()
        {
            if (Transaction == null)
            {
                throw new ArgumentException(nameof(Transaction));
            }
            Transaction.Commit();
            Transaction = null;
        }

        public void Rollback()
        {
            if (Transaction == null)
            {
                throw new ArgumentException(nameof(Transaction));
            }
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction.Dispose();
            }

            _connection.Close();
            _connection.Dispose();
        }
    }
}
