using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IService<UsersDto> service;
        // GET: CustomersController
        public UsersController(IService<UsersDto> service)
        {
            this.service = service;
        }
        // GET: CustomersController/Details/5
        [HttpGet]
        public async Task<List<UsersDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<UsersDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        [HttpPost]
        public async Task Post([FromForm] UsersDto usersDto)
        {
            await service.AddAsync(usersDto);
        }
        [HttpPut("{id}")]
        public async Task Put(int id,[FromForm] UsersDto usersDto)
        {
            await service.UpdateAsync(id,usersDto);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}
