using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRental.Entity
{
    public class LocationEntity
    {
        public string BookingId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
