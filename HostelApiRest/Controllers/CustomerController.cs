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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> ReadAsync()
        {
            var response = new List<CustomerDto>();

            List<Customer> list = await this.unitOfWork.CustomerRepository.ListAsync();
            list.ForEach(dto => response.Add(this.mapper.Map<CustomerDto>(dto)));
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<CustomerDto> ReadAsync(int id)
        {
            var model = await this.unitOfWork.CustomerRepository.SearchAsync(id);
            var dto = this.mapper.Map<CustomerDto>(model);
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
