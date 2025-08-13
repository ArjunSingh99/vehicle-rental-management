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
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDapperContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleRepository(IDapperContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VehicleEntity>> GetVehicles(VehicleEntity entity)
        {
            var query = new StringBuilder(@"
SELECT 
id As Id,
model As Model,
registration_number AS RegistrationNumber,
booked AS Booked
FROM vehicle
WHERE 1=1 ");

            if (!string.IsNullOrEmpty(entity.Model))
            {
                query.AppendLine(" AND model = @Model ");
            }

            if (!string.IsNullOrEmpty(entity.RegistrationNumber))
            {
                query.AppendLine(" AND registration_number = @RegistrationNumber ");
            }


            var param = new
            {
                entity.Model,
                entity.RegistrationNumber,
            };

            using var connection = _context.GetConnection();
            var result = await connection.QueryAsync<VehicleEntity>(query.ToString(), param);

            return result ?? [];
        }

        public async Task<IEnumerable<VehicleEntity>> GetAvailableVehicles(BookingEntity entity, int? pageSize = default, int? pageNumber = default)
        {
            var query = new StringBuilder(@"
SELECT 
    v.id AS Id,
    v.model AS Model,
    v.registration_number AS RegistrationNumber
FROM vehicle v
WHERE 1=1 
AND v.model = @VehicleModel
AND NOT EXISTS (
    SELECT 1
    FROM rentals r
    WHERE r.vehicle_registration_number = v.registration_number
      AND r.vehicle_model = v.model
       AND r.status = 'Booked'
      AND pickup_date <= @ReturnDate::Date
	AND return_date >= @PickupDate::Date
)");

            if (pageSize.HasValue)
            {
                query.AppendLine(" LIMIT @pageSize ");
                if (pageNumber.HasValue)
                    query.AppendLine(" OFFSET @pageSize * @pageNumber ");
            }
            var param = new
            {
                entity.VehicleModel,
                entity.PickupDate,
                entity.ReturnDate,
                pageSize,
                pageNumber
            };

            using var connection = _context.GetConnection();
            var result = await connection.QueryAsync<VehicleEntity>(query.ToString(), param);
            return result ?? [];
        }

        public async Task<IEnumerable<VehicleEntity>> GetVehiclesList()
        {
            var query = new StringBuilder(@"
SELECT
id AS Id,
model AS Model,
registration_number AS RegistrationNumber
from vehicle
");
            using var connection = _context.GetConnection();
            var result = await connection.QueryAsync<VehicleEntity>(query.ToString());
            return result ?? [];
        }

        public async Task<bool> UpdateVehicleBookingStatus(string registrationNumber, bool status)
        {
            var query = new StringBuilder(@"
UPDATE vehicle
SET booked = @status
WHERE registration_number = @registrationNumber
");
            var param = new
            {
                registrationNumber,
                status
            };

            var connection = _unitOfWork.GetConnection();
            var transaction = _unitOfWork.GetCurrentTransaction();

            if (transaction is null)
            {
                _unitOfWork.BeginTransaction();
            }

            var result = await connection.ExecuteAsync(query.ToString(), param, transaction);

            return result >= 1;
        }

        #region transaction related methods

        public void Commit()
        {
            _unitOfWork.Commit();
        }
        public void RollBack()
        {
            _unitOfWork.Rollback();
        }

        #endregion
    }
}
