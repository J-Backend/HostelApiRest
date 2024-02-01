using AutoMapper;
using HostelModels.Models.Db;
using HostelModels.Models.Dto;

namespace HostelApiRest.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<NewHotelDto, Hotel>();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<NewCustomerDto, Customer>();

            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<NewBookingDto, Booking>();
        }
    }
}
