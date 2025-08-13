using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;
using VehicleRental.Repository.Interface;
using VehicleRental.Service.Interface;
using static Dapper.SqlMapper;

namespace VehicleRental.Service.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public BookingService(IBookingRepository bookingRepository, IVehicleRepository vehicleRepository)
        {
            _bookingRepository = bookingRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<BookingEntity>> GetBookings(BookingEntity entity, int? pageSize, int? pageNumber)
        {
            var result = await _bookingRepository.GetAllBookings(entity, pageSize, pageNumber);

            return result;
        }

        public async Task<BookingEntity> GetBookingById(string id)
        {
            var bookingFilter = new BookingEntity
            {
                BookingId = id
            };

            var result = await _bookingRepository.GetAllBookings(bookingFilter, default, default);

            return result.FirstOrDefault();
        }

        public async Task<string> CreateBooking(BookingEntity entity)
        {
            // todo: add validation for valid customer

            var availableVehicle = await AvailableVehicle(entity);

            if (availableVehicle == default)
            {
                // todo: take some action here??
                return default;
            }

            entity.VehicleRegistrationNumber = availableVehicle.RegistrationNumber;
            var bookingId = await _bookingRepository.CreateBooking(entity);

            return bookingId;
        }

        public async Task<bool> CancelBooking(string id)
        {
            var vehicle = await GetVehicleByBookingId(id);

            var isValidCancellation = await ValidateCancellation(id, vehicle);

            if (!isValidCancellation)
            {
                return default;
            }

            var result = await _bookingRepository.CancelBooking(id);

            return result;
        }


        #region private methods

        private async Task<VehicleEntity> AvailableVehicle(BookingEntity entity)
        {

            var bookingFilter = new BookingEntity
            {
                VehicleModel = entity.VehicleModel,
                PickupDate = entity.PickupDate,
                ReturnDate = entity.ReturnDate,
            };

            var availableVehicle = await _vehicleRepository.GetAvailableVehicles(bookingFilter, pageSize: 1);

            return availableVehicle.FirstOrDefault();
        }

        private async Task<VehicleEntity> GetVehicleByBookingId(string id)
        {
            var booking = await GetBookingById(id);

            var vehicleFilter = new VehicleEntity
            {
                RegistrationNumber = booking.VehicleRegistrationNumber
            };

            var vehicle = await _vehicleRepository.GetVehicles(vehicleFilter);

            return vehicle.FirstOrDefault();
        }

        private async Task<bool> ValidateCancellation(string bookingId, VehicleEntity vehicle)
        {
            if (vehicle == default)
            {
                return false;
            }

            var booking = await GetBookingById(bookingId);

            if (booking.ReturnDate < DateTime.Today)
            {
                return false;
            }

            if (!string.Equals(booking.BookingStatus, "Booked", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            // todo: can we add more validation here?

            return true;
        }
        #endregion
    }
}