using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IService<AuditLogsDto> service;
        // GET: CustomersController
        public AuditLogsController(IService<AuditLogsDto> service)
        {
            this.service = service;
        }
        // GET: CustomersController/Details/5
        [HttpGet]
        public async Task<List<AuditLogsDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<AuditLogsDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        [HttpPost]
        public async Task Post([FromForm] AuditLogsDto auditLogsDto)
        {
            await service.AddAsync(auditLogsDto);
        }
        [HttpPut("{id}")]
        public async Task Put(int id,[FromForm] AuditLogsDto auditLogsDto)
        {
            await service.UpdateAsync(id,auditLogsDto);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}
