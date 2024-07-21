using Common.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using static Dropbox.Api.Files.ListRevisionsMode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        private readonly IService<CustomersDto> service;
        
        public CustomersController(IService<CustomersDto> service)
        {
            this.service = service;
        }
        
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
        [HttpGet("userId{userId}")]
        public async Task<int> GetByUserId(int userId)
        {
            var allCustomers = await Get();
            var customer = allCustomers.Find(x => x.UserId == userId);
            return customer.Id;
        }

        //[HttpPost]
        //public async Task Post([FromBody] CustomersDto customersDto)
        //{
        //    await service.AddAsync(customersDto);
        //}

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] CustomersDto customersDto)
        {
            Console.WriteLine("in post customer");
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
