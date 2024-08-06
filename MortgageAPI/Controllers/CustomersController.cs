using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;
using static Dropbox.Api.Files.ListRevisionsMode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
        private readonly IService<CustomersDto> service;
        private readonly IService<LeadsDto> leadService;

        public CustomersController(IService<CustomersDto> service, IService<LeadsDto> leadService)
        {
            this.service = service;
            this.leadService = leadService;
        }
        
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]

        public async Task<List<CustomersDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        [Authorize]

        public async Task<CustomersDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        [HttpGet("userId{userId}")]

        public async Task<int> GetByUserId(int userId)
        {
            Console.WriteLine("userid="+userId);
            var allCustomers = await Get();
            var customer = allCustomers.Find(x => x.UserId == userId);
            if(customer == null)
            {
                return -1;
            }
            return customer.Id;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddItemAsync([FromBody] CustomersDto customersDto)
        {
            return await AddItemAsyncPrivate(customersDto);
        }
        private async Task<IActionResult> AddItemAsyncPrivate(CustomersDto customersDto)
        {
            var addedObject = await service.AddAsync(customersDto);
            return Ok(addedObject);
        }
        [HttpPost("Lead{leadId}")]
        public async Task<IActionResult> AddItemAsync([FromBody] CustomersDto customersDto, int leadId)
        {
            var leadsController = new LeadsController(leadService);
            LeadsDto leadDto = await leadsController.Get(leadId);
            if (leadDto != null)
            {
                if (leadDto.Expiration >= DateTime.Now)
                {
                    return await AddItemAsyncPrivate(customersDto);
                }
                return BadRequest("The lead has expired and cannot be used to add a new user.");
            }

            return NotFound("The lead does not exist.");
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] CustomersDto customersDto)
        {
            var updatedObject = await service.UpdateItemAsync(id, customersDto);
            return Ok(updatedObject);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]

        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}
