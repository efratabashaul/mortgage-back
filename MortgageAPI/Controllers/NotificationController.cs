using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interface;
using Service.Interfaces;

namespace MortgageAPI.Controllers
{
  [ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
        private readonly IService<NotificationDto> service;

        public NotificationController(IService<NotificationDto> service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<List<NotificationDto>> Get()
        {
            Console.WriteLine("in moti controller");
            return await service.GetAllAsync();
        }
        [HttpGet("{userId}")]
        public async Task<NotificationDto[]> Get(int userId)
        {
            var notifications = await service.GetAllAsync();
            var filteredNotifications = notifications.Where(n => n.UserId == userId).ToArray();
            return filteredNotifications;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemAsync( NotificationDto notificationDto)
        {
            Console.WriteLine("in post notification");
            var addedObject = await service.AddAsync(notificationDto);
            return Ok(addedObject);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] NotificationDto customersDto)
        {
            Console.WriteLine("in put notifi"+customersDto.IsRead);
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
