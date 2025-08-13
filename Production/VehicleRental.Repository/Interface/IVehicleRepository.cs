using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;

namespace VehicleRental.Repository.Interface
{
    public interface IVehicleRepository
    {
        void Commit();
        Task<IEnumerable<VehicleEntity>> GetAvailableVehicles(BookingEntity entity, int? pageSize = default, int? pageNumber = default);
        Task<IEnumerable<VehicleEntity>> GetVehicles(VehicleEntity entity);
        Task<IEnumerable<VehicleEntity>> GetVehiclesList();
        void RollBack();
        Task<bool> UpdateVehicleBookingStatus(string registrationNumber, bool status);
    }
}
