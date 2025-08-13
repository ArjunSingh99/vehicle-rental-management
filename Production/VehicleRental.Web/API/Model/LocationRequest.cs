using System.ComponentModel.DataAnnotations;

namespace VehicleRental.Web.API.Model
{
    public class LocationRequest
    {
        [Required]
        public string BookingId { get; set; }
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public double? Latitude { get; set; }
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
        public double? Longitude { get; set; }
    }
}
