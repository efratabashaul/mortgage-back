using Common.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        private readonly IService<CustomersDto> service;
        // GET: CustomersController
        public CustomersController(IService<CustomersDto> service)
        {
            this.service = service;
        }
        // GET: CustomersController/Details/5
        [HttpGet]
        public async Task<List<CustomersDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<CustomersDto> Get(int id)
        {
            return await service.GetAsync(id);
        }

        //[HttpPost]
        //public async Task Post([FromBody] CustomersDto customersDto)
        //{
        //    await service.AddAsync(customersDto);
        //}

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] CustomersDto customersDto)
        {
            var addedObject = await service.AddAsync(customersDto);
            return Ok(addedObject);
        }

        //[HttpPut("{id}")]
        //public async Task Put(int id,[FromBody] CustomersDto customersDto)
        //{
        //    await service.UpdateAsync(id,customersDto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] CustomersDto customersDto)
        {
            var updatedObject = await service.UpdateItemAsync(id, customersDto);
            return Ok(updatedObject);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}
