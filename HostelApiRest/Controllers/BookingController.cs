using AutoMapper;
using HostelApiRest.Contracts;
using HostelModels.Models.Db;
using HostelModels.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BookingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BookingDto>> ReadAsync()
        {
            var response = new List<BookingDto>();

            List<Booking> list = await this.unitOfWork.BookingRepository.ListAsync();
            list.ForEach(dto => response.Add(this.mapper.Map<BookingDto>(dto)));
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<BookingDto> ReadAsync(int id)
        {
            var model = await this.unitOfWork.BookingRepository.SearchAsync(id);
            var dto = this.mapper.Map<BookingDto>(model);
            return model == null ? null : dto;
        }

        [HttpPost]
        public async Task<int> CreateAsync(NewBookingDto dto)
        {
            var model = this.mapper.Map<Booking>(dto);
            var state = await this.unitOfWork.BookingRepository.AddAsync(model);
            return state;
        }

        [HttpPut]
        public async Task<int> UpdateAsync(BookingDto dto)
        {
            var model = this.mapper.Map<Booking>(dto);
            var state = await this.unitOfWork.BookingRepository.ChangeAsync(model);
            return state;
        }

        [HttpDelete("{id:int}")]
        public async Task<int> DeleteAsync(int id)
        {
            var state = await this.unitOfWork.BookingRepository.EraseAsync(id);
            return state;
        }
    }
}
