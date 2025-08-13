using AutoMapper;
using VehicleRental.Entity;
using VehicleRental.Web.API.Model;

namespace VehicleRental.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BookingRequest, BookingEntity>();
        }
    }
}
