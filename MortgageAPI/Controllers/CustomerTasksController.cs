using AutoMapper;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interface;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTasksControllercs: ControllerBase
    {
        private readonly IService<CustomerTasksDto> service;
        
        public CustomerTasksControllercs(IService<CustomerTasksDto> service)
        {
            this.service = service;
            
        }
        
        [HttpGet]
        [Authorize]

        public async Task<List<CustomerTasksDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        [Authorize]

        public async Task<CustomerTasksDto> Get(int id)
        {
            return await service.GetAsync(id);
        }


        [HttpGet("customerId/{id}")]
        [Authorize]

        public async Task<List<CustomerTasksDto>> GetByCustomer(int id)
        {
            var allcustomerTask = await Get();
            var customerTaskId = allcustomerTask.Where(x => x.Customer_Id == id).ToList();
            return customerTaskId;
        }

        //[HttpPost]
        //public async Task Post([FromForm] CustomerTasksDto customerTasksDto)
        //{
        //    await service.AddAsync(customerTasksDto);
        //}

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] CustomerTasksDto customerTasksDto)
        {
            var addedObject = await service.AddAsync(customerTasksDto);
            return Ok(addedObject);
        }


        //[HttpPut("{id}")]
        //public async Task Put(int id, [FromForm] CustomerTasksDto customerTasksDto)
        //{
        //    await service.UpdateAsync(id ,customerTasksDto);
        //}

        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] CustomerTasksDto customerTasksDto)
        {
            var updatedObject = await service.UpdateItemAsync(id, customerTasksDto);
            return Ok(updatedObject);
        }



        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}


