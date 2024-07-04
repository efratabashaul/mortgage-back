using Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace MortgageAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class LeadsController : ControllerBase
    {
        private readonly IService<LeadsDto> service;
        // GET: LeadsController
        public LeadsController(IService<LeadsDto> service)
        {
            this.service = service;
        }
        // GET: LeadsController/Details/5
        [HttpGet]
        public async Task<List<LeadsDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<LeadsDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        //[HttpPost]
        //public async Task Post([FromBody] LeadsDto leadsDto)
        //{
        //    await service.AddAsync( leadsDto);
        //}

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromForm] LeadsDto leadsDto)
        {
            var addedObject = await service.AddAsync(leadsDto);
            return Ok(addedObject);
        }


        //[HttpPut("{id}")]
        //public async Task Put(int id, [FromForm] LeadsDto leadsDto)
        //{
        //    await service.UpdateAsync(id, leadsDto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromForm] LeadsDto leadsDto)
        {
            var updatedObject = await service.UpdateItemAsync(id, leadsDto);
            return Ok(updatedObject);
        }



        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}
