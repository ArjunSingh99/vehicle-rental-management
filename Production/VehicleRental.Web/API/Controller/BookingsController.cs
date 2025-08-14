using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleRental.Entity;
using VehicleRental.Service.Interface;
using VehicleRental.Web.API.Model;

namespace VehicleRental.Web.API.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        // todo: potential improvements
        /*
         -> logging
         -> authorization
         -> better input validation
         -> better error handling
         -> exception handling middleware
         -> unit tests
         -> input sanitization
         */

        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the list of all the bookings, optionally filtered through pagination
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>A list of all the bookings</returns>
        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] BookingEntity entity, int? pageSize, int? pageNumber)
        {
            var result = await _bookingService.GetBookings(entity, pageSize, pageNumber);
            return Ok(result);
        }

        /// <summary>
        /// Returns the details of a booking by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The details of a booking by id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingById(string id)
        {
            if (!int.TryParse(id, out _))
            {
                ModelState.AddModelError(nameof(id), "Booking ID must be a numeric value");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookingService.GetBookingById(id);

            if (result == default)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates a booking for a customer against a vehicle
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The booking id</returns>
        /// <response code="201">Returns the booking id</response>
        /// <response code="400">If the vehicle is not available for booking</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking(BookingRequest request)
        {
            if (request.PickupDate.HasValue && request.ReturnDate.HasValue && request.ReturnDate.Value.Date <= request.PickupDate.Value.Date)
            {
                ModelState.AddModelError("ReturnDate", "Return date must be after pickup date.");
            }

            if (request.PickupDate.HasValue && request.PickupDate.Value.Date < DateTime.UtcNow.Date)
            {
                ModelState.AddModelError("PickupDate", "Pickup date cannot be in the past.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inputMapped = _mapper.Map<BookingEntity>(request);

            try
            {
                var result = await _bookingService.CreateBooking(inputMapped);

                var response = $"Booking created successfully. Booking ID: {result}";
                return Created("BookingId", response);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Cancels a booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelBooking(string id)
        {
            if (!int.TryParse(id, out _))
            {
                ModelState.AddModelError(nameof(id), "Booking ID must be a numeric value");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookingService.CancelBooking(id);

            if (result == default)
            {
                return BadRequest("Unable to cancel booking");
            }

            return NoContent();
        }

        /// <summary>
        /// Returns the location history of a booking by its booking id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of coordinates (lat long)</returns>
        [HttpGet("{id}/location/history")]
        public async Task<IActionResult> GetLocationHistory(string id)
        {
            var result = await _bookingService.GetLocationHistory(id);

            return Ok(result);
        }
    }
}
