using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController: ControllerBase
    {
        private readonly IService<NotificationsDto> service;
        // GET: CustomersController
        public NotificationsController(IService<NotificationsDto> service)
        {
            this.service = service;
        }
        // GET: CustomersController/Details/5
        [HttpGet]
        public async Task<List<NotificationsDto>> Get()
        {
            return await service.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<NotificationsDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        [HttpPost]
        public async Task Post([FromForm] NotificationsDto notificationsDto)
        {
            await service.AddAsync(notificationsDto);
        }
        [HttpPut("{id}")]
        public async Task Put(int id,[FromForm] NotificationsDto notificationsDto)
        {
            await service.UpdateAsync(id,notificationsDto);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }
    }
}



