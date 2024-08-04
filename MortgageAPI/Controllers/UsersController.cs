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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;  // one more package for working with JSON



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILoginService service;
        private IConfiguration _configuration;
        private readonly IService<LeadsDto> leadService;

        // GET: CustomersController
        public UsersController(ILoginService service, IConfiguration config, HttpClient httpClient, IService<LeadsDto> leadService)
        {
            this.service = service;
            this._configuration = config;
            _httpClient = httpClient;
            this.leadService = leadService;
        }


        private async Task<UsersDto> Authenticate(string Email, string Password)
        {
            return this.service.Login(Email, Password);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsersDto user)
        {
            var u = await Authenticate(user.Email, user.Password);
            if (u != null)
            {
                var token =await GenerateAsync(u);
                return Ok(new { token });//token
            }
            return NotFound("user not found");
        }

        [HttpPost("token")]
        public async Task<string> GenerateAsync(UsersDto user)
        {
            int customerId = -1;
            if (user.Role == Repositories.Entities.Role.Customer)
            {
                var response = await _httpClient.GetAsync($"https://localhost:7055/api/Customers/userId{user.Id}");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode).ToString();
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                customerId = JsonConvert.DeserializeObject<int>(jsonString);
            }

            //מפתח להצפנה
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //אלגוריתם להצפנה
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier,user.UserName)
            ,new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("userid",user.Id.ToString()),
            new Claim("customerId",customerId.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // GET: CustomersController/Details/5
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<List<UsersDto>> Get()
        {
            return await service.GetAllAsync();
        }

        [Authorize(Policy = "AdminPolicy")]

        [HttpGet("{id}")]
        public async Task<UsersDto> Get(int id)
        {
            return await service.GetAsync(id);
        }
        [Authorize(Policy = "AdminPolicy")]

        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] UsersDto usersDto)
        {

            return await AddUserPrivate(usersDto);
        }

        [HttpPost("Lead{leadId}")]
        public async Task<IActionResult> AddItemAsync([FromBody] UsersDto usersDto, int leadId)
        {
            var leadsController = new LeadsController(leadService);
            LeadsDto leadDto = await leadsController.Get(leadId);
            if (leadDto != null)
            {
                if (leadDto.Expiration >= DateTime.Now)
                {
                    return await AddUserPrivate(usersDto);
                }
                return BadRequest("The lead has expired and cannot be used to add a new user.");
            }

            return BadRequest("The lead does not exist.");
        }

        private async Task<IActionResult> AddUserPrivate(UsersDto usersDto)
        {
            var addedObject = await service.AddAsync(usersDto);
            return Ok(addedObject);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] UsersDto usersDto)
        {
            Console.WriteLine("ccjcjcj");
            var updatedObject = await service.UpdateItemAsync(id, usersDto);
            return Ok(updatedObject);
        }
        [Authorize(Policy = "AdminPolicy")]

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await service.DeleteAsync(id);
            
        }
    

    }
}