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
    public class BookingRepository : IBookingRepository
    {
        private readonly IDapperContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public BookingRepository(IDapperContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookingEntity>> GetAllBookings(BookingEntity entity, int? pageSize = null, int? pageNumber = null)
        {

            #region query construction
            var query = new StringBuilder(@"
SELECT
id AS BookingId,
customer_name AS CustomerName,
customer_phone_number AS CustomerPhoneNumber,
vehicle_registration_number AS VehicleRegistrationNumber,
vehicle_model AS VehicleModel,
pickup_date AS PickupDate,
return_date AS ReturnDate,
daily_rate AS DailyRate,
status AS BookingStatus
FROM rentals
WHERE 1=1 
");
            if (!string.IsNullOrEmpty(entity.BookingId))
            {
                query.AppendLine(" AND id = @BookingId::int");
            }

            if (!string.IsNullOrEmpty(entity.CustomerName))
            {
                query.AppendLine(" AND customer_name = @CustomerName ");
            }

            if (!string.IsNullOrEmpty(entity.CustomerPhoneNumber))
            {
                query.AppendLine(" AND customer_phone_number = @CustomerPhoneNumber ");
            }

            if (!string.IsNullOrEmpty(entity.VehicleRegistrationNumber))
            {
                query.AppendLine(" AND vehicle_registration_number = @VehicleRegistrationNumber ");
            }

            if (!string.IsNullOrEmpty(entity.VehicleModel))
            {
                query.AppendLine(" AND vehicle_model = @VehicleModel ");
            }

            if (entity.PickupDate != default)
            {
                query.AppendLine(" AND pickup_date >= @PickupDate");
            }

            if (entity.ReturnDate != default)
            {
                query.AppendLine(" AND return_date <= @ReturnDate");
            }

            if (!string.IsNullOrEmpty(entity.BookingStatus))
            {
                query.AppendLine(" AND status = @BookingStatus");
            }

            if (pageSize != default)
            {
                query.AppendLine(" LIMIT @pageSize ");

                if (pageNumber != default)
                    query.AppendLine(" OFFSET @pageSize * @pageNumber ");
            }
            #endregion

            var param = new
            {
                entity.BookingId,
                entity.CustomerName,
                entity.CustomerPhoneNumber,
                entity.VehicleRegistrationNumber,
                entity.VehicleModel,
                entity.PickupDate,
                entity.ReturnDate,
                entity.BookingStatus,
                pageSize,
                pageNumber,
            };

            using var connection = _context.GetConnection();
            var result = await connection.QueryAsync<BookingEntity>(query.ToString(), param);

            return result ?? [];
        }

        // todo: remove this method
        public async Task<IEnumerable<BookingEntity>> GetAvailableBookings(BookingEntity entity, int? pageSize = null, int? pageNumber = null)
        {

            #region query construction
            var query = new StringBuilder(@"
SELECT
id AS BookingId,
customer_name AS CustomerName,
customer_phone_number AS CustomerPhoneNumber,
vehicle_registration_number AS VehicleRegistrationNumber,
vehicle_model AS VehicleModel,
pickup_date AS PickupDate,
return_date AS ReturnDate,
daily_rate AS DailyRate,
status AS BookingStatus
FROM rentals
WHERE 1=1 
");
            // Inverse filters for PickupDate and ReturnDate
            if (entity.PickupDate != default)
            {
                query.AppendLine(" AND pickup_date < @PickupDate");
            }

            if (entity.ReturnDate != default)
            {
                query.AppendLine(" AND return_date > @ReturnDate");
            }

            // Inverse filter for BookingStatus
            if (!string.IsNullOrEmpty(entity.BookingStatus))
            {
                query.AppendLine(" AND status != @BookingStatus");
            }

            if (pageSize != default)
            {
                query.AppendLine(" LIMIT @pageSize ");

                if (pageNumber != default)
                    query.AppendLine(" OFFSET @pageSize * @pageNumber ");
            }
            #endregion

            var param = new
            {
                //entity.BookingId,
                //entity.CustomerName,
                //entity.CustomerPhoneNumber,
                //entity.VehicleRegistrationNumber,
                entity.VehicleModel,
                entity.PickupDate,
                entity.ReturnDate,
                entity.BookingStatus,
                pageSize,
                pageNumber,
            };

            using var connection = _context.GetConnection();
            var result = await connection.QueryAsync<BookingEntity>(query.ToString(), param);

            return result ?? [];
        }

        public async Task<string> CreateBooking(BookingEntity entity)
        {
            var query = new StringBuilder(@"
INSERT INTO rentals (
customer_name,
customer_phone_number,
vehicle_registration_number,
vehicle_model,
pickup_date,
return_date,
daily_rate
)
VALUES (
@CustomerName,
@CustomerPhoneNumber,
@VehicleRegistrationNumber,
@VehicleModel,
@PickupDate,
@ReturnDate,
@DailyRate
)
RETURNING id::text
");

            var param = new
            {
                entity.CustomerName,
                entity.CustomerPhoneNumber,
                entity.VehicleRegistrationNumber,
                entity.VehicleModel,
                entity.PickupDate,
                entity.ReturnDate,
                entity.DailyRate
            };

            //var connection = _unitOfWork.GetConnection();

            //var transaction = _unitOfWork.GetCurrentTransaction();

            //if (transaction is null)
            //{
            //    _unitOfWork.BeginTransaction();
            //}

            using var connection = _context.GetConnection();
            var result = await connection.ExecuteScalarAsync<string>(query.ToString(), param);

            return result;
        }

        public async Task<bool> CancelBooking(string id)
        {
            var query = new StringBuilder(@"
UPDATE rentals
SET status = 'Cancelled'
WHERE id = (@id::int)
");

            var param = new
            {
                id
            };

            //var connection = _unitOfWork.GetConnection();

            //var transaction = _unitOfWork.GetCurrentTransaction();

            //if (transaction is null)
            //{
            //    _unitOfWork.BeginTransaction();
            //}

            using var connection = _context.GetConnection();

            var result = await connection.ExecuteAsync(query.ToString(), param);

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
