using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;

namespace VehicleRental.Service.Interface
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingEntity>> GetBookings(BookingEntity entity, int? pageSize, int? pageNumber);
        Task<BookingEntity> GetBookingById(string id);
        Task<string> CreateBooking(BookingEntity entity);
        Task<bool> CancelBooking(string id);
        Task<IEnumerable<LocationEntity>> GetLocationHistory(string bookingId);
    }
}
