using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interface;

namespace MortgageAPI.Controllers
{
  [ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IContext _context;

    public NotificationController(IContext context)
    {
        _context = context;
    }

 
}

}
