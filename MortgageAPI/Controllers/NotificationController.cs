using Common.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AdminPolicy")]

        public async Task<List<NotificationDto>> Get()
        {
            Console.WriteLine("in moti controller");
            return await service.GetAllAsync();
        }
        [HttpGet("{userId}")]
        [Authorize]

        public async Task<NotificationDto[]> Get(int userId)
        {
            var notifications = await service.GetAllAsync();
            var filteredNotifications = notifications.Where(n => n.UserId == userId).ToArray();
            return filteredNotifications;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]

        public async Task<IActionResult> AddItemAsync( NotificationDto notificationDto)
        {
            Console.WriteLine("in post notification");
            var addedObject = await service.AddAsync(notificationDto);
            return Ok(addedObject);
        }
        [HttpPut("{id}")]

        [Authorize]

        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] NotificationDto notificationsDto)
        {
            var updatedObject = await service.UpdateItemAsync(id, notificationsDto);
            return Ok(updatedObject);
        }
        [HttpPut()]
        public async Task<IActionResult> UpdateItemsToReadAsync([FromBody] NotificationDto[] notificationsDto)
        {
            NotificationDto[] updated=[];
            foreach (var item in notificationsDto)
            {
                Console.WriteLine(item.Message);
                item.IsRead = true;
                var updatedObject = await service.UpdateItemAsync(item.ID, item);
                updated.Append(updatedObject);
            }
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]

        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
        }

    }

}
