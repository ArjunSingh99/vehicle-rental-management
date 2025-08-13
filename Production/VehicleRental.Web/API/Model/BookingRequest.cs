using System.ComponentModel.DataAnnotations;

namespace VehicleRental.Web.API.Model
{
    public class BookingRequest
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public string VehicleModel { get; set; }
        [Required]
        public DateTime? PickupDate { get; set; }
        [Required]
        public DateTime? ReturnDate { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Daily rate must be a positive value.")]
        public double DailyRate { get; set; }
        [Range(-90, 90)]
        public double? StartLatitude { get; set; }
        [Range(-180, 180)]
        public double? StartLongitude { get; set; }
    }
}
