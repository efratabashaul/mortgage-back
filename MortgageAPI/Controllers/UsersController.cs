using Common.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly ILoginService service;
        // GET: CustomersController
        public UsersController(ILoginService service)
        {
            this.service = service;
        }



        private async Task<UsersDto> Authenticate(string Email, string Password)
        {
            return this.service.Login(Email, Password);
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login([FromBody] UsersDto user)
        {
            var u = await Authenticate(user.Email, user.Password);
            if (u != null)
            {
                //var token = Generate(u);
                return Ok();//token
            }
            return NotFound("user not found");
        }




        // GET: CustomersController/Details/5
        [HttpGet]
        public async Task<List<UsersDto>> Get()
        {
            return await service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<UsersDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        //[HttpPost]
        //public async Task Post([FromForm] UsersDto usersDto)
        //{
        //    await service.AddAsync(usersDto);
        //}

        //[HttpPost("result")]

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] UsersDto usersDto)
        {
            var addedObject = await service.AddAsync(usersDto);
            return Ok(addedObject);
        }

        //[HttpPut("{id}")]
        //public async Task Put(int id,[FromForm] UsersDto usersDto)
        //{
        //    await service.UpdateAsync(id,usersDto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] UsersDto usersDto)
        {
            var updatedObject = await service.UpdateItemAsync(id, usersDto);
            return Ok(updatedObject);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
            //121212
        }
    }
}
