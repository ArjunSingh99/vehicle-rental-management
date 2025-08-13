using Microsoft.AspNetCore.Mvc;
using VehicleRental.Service.Interface;

namespace VehicleRental.Web.API.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Returns the list of all vehicles
        /// </summary>
        /// <returns>List of all vehicles</returns>
        /// <response code="200">Returns the list of all vehicles</response>
        /// <response code="404">If no vehicles are found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehiclesList()
        {
            var result = await _vehicleService.GetVehiclesList();

            if (result == default || !result.Any())
            {
                return NotFound("No vehicles found.");
            }

            return Ok(result);
        }
    }
}
