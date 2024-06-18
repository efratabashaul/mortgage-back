using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTasksControllercs: ControllerBase
    {
        private readonly IService<CustomerTasksDto> service;
        // GET: CustomersController
        public CustomerTasksControllercs(IService<CustomerTasksDto> service)
        {
            this.service = service;
        }
        // GET: CustomersController/Details/5
        [HttpGet]
        public async Task<List<CustomerTasksDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<CustomerTasksDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        [HttpPost]
        public async Task Post([FromForm] CustomerTasksDto customerTasksDto)
        {
            await service.AddAsync(customerTasksDto);
        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromForm] CustomerTasksDto customerTasksDto)
        {
            await service.UpdateAsync(id ,customerTasksDto);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}


