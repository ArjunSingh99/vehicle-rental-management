using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Entity;
using VehicleRental.Repository.Interface;
using VehicleRental.Service.Interface;

namespace VehicleRental.Service.Implementation
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public async Task<IEnumerable<VehicleEntity>> GetVehiclesList()
        {
            var result = await _vehicleRepository.GetVehiclesList();

            return result;
        }
    }
}
