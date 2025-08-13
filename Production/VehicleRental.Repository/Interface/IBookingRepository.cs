using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;

namespace VehicleRental.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingEntity>> GetAllBookings(BookingEntity entity, int? pageSize = null, int? pageNumber = null);
        Task<string> CreateBooking(BookingEntity entity);
        Task<bool> CancelBooking(string id);
        void Commit();
        void RollBack();
        Task<IEnumerable<BookingEntity>> GetAvailableBookings(BookingEntity entity, int? pageSize = null, int? pageNumber = null);
    }
}
