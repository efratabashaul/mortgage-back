using Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypesController : ControllerBase
    {
        private readonly IService<DocumentTypesDto> service;
        // GET: DocumentTypesController
        public DocumentTypesController(IService<DocumentTypesDto> service)
        {
            this.service = service;
        }
        // GET: DocumentTypesController/Details/5
        [HttpGet]
        public async Task<List<DocumentTypesDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<DocumentTypesDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        //[HttpPost]
        //public async Task Post([FromBody] DocumentTypesDto documentTypesDto)
        //{
        //    await service.AddAsync(documentTypesDto);
        //}

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] DocumentTypesDto documentTypesDto)
        {
            var addedObject = await service.AddAsync(documentTypesDto);
            return Ok(addedObject);
        }

        //[HttpPut("{id}")]
        //public async Task Put(int id, [FromForm] DocumentTypesDto documentTypesDto)
        //{
        //    await service.UpdateAsync(id, documentTypesDto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] DocumentTypesDto documentTypesDto)
        {
            var updatedObject = await service.UpdateItemAsync(id, documentTypesDto);
            return Ok(updatedObject);
        }


        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}
