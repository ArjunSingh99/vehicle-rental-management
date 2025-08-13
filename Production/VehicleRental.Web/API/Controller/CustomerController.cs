using Microsoft.AspNetCore.Mvc;
using VehicleRental.Service.Interface;

namespace VehicleRental.Web.API.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Returns the list of all customers
        /// </summary>
        /// <returns>A list of all customers</returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.GetCustomersList();

            if (result == default || !result.Any())
            {
                return NotFound("No customers found.");
            }

            return Ok(result);
        }
    }
}
