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
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public HotelController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<HotelDto>> ReadAsync()
        {
            var response = new List<HotelDto>();

            List<Hotel> list = await this.unitOfWork.HotelRepository.ListAsync();
            list.ForEach(dto => response.Add(this.mapper.Map<HotelDto>(dto)));
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<HotelDto> ReadAsync(int id) 
        {
            var model = await this.unitOfWork.HotelRepository.SearchAsync(id);
            var dto = this.mapper.Map<HotelDto>(model);
            return model == null ? null : dto;
        }

        [HttpPost]
        public async Task<int> CreateAsync(NewHotelDto dto) 
        {
            var model = this.mapper.Map<Hotel>(dto);
            var state = await this.unitOfWork.HotelRepository.AddAsync(model);
            return state;
        }

        [HttpPut]
        public async Task<int> UpdateAsync(HotelDto dto)
        {
            var model = this.mapper.Map<Hotel>(dto);
            var state = await this.unitOfWork.HotelRepository.ChangeAsync(model);
            return state;
        }

        [HttpDelete("{id:int}")]
        public async Task<int> DeleteAsync(int id) 
        {
            var state = await this.unitOfWork.HotelRepository.EraseAsync(id);
            return state;
        }
    }
}
