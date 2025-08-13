using VehicleRental.Repository.DbContext;
using VehicleRental.Repository.Implementation;
using VehicleRental.Repository.Interface;
using VehicleRental.Service.Implementation;
using VehicleRental.Service.Interface;

namespace VehicleRental.Web.Helpers
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            #region service
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<ICustomerService, CustomerService>();
            #endregion

            #region repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            #endregion
        }
    }
}
