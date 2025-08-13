using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRental.Entity
{
    public class BookingEntity
    {
        public string BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string VehicleModel { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double? DailyRate { get; set; }
        public string BookingStatus { get; set; }
    }
}
