using Common.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.CodeDom.Compiler;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly ILoginService service;
        private IConfiguration _configuration;

        // GET: CustomersController
        public UsersController(ILoginService service, IConfiguration config)
        {
            this.service = service;
            this._configuration = config;
        }


        private async Task<UsersDto> Authenticate(string Email, string Password)
        {
            return this.service.Login(Email, Password);
        }

        [HttpGet("{email}/{password}")]
        public async Task<ActionResult> Login(string email,string password)
        {
            var u = await Authenticate(email, password);
            if (u != null)
            {
                var token = Generate(u);
                return Ok(token);//token
            }
            return NotFound("user not found");
        }

        private string Generate(UsersDto user)
        {
            //מפתח להצפנה
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //אלגוריתם להצפנה
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier,user.UserName)
            ,new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("userid",user.Id.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
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
            Console.WriteLine(  usersDto.UserName);
            var addedObject = await service.AddAsync(usersDto);
            Console.WriteLine( "succeded?");
            Console.WriteLine(addedObject.UserName+" "+ addedObject.Id);
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
            
        }
    }
}
