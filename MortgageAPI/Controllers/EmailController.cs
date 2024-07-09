using AutoMapper;
using Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using Repositories.Entities;
using Repositories.Interface;
using Service.Interfaces;
using Service.Services;
using System.Security.Cryptography;
using IEmailService = Service.Interfaces.IEmailService;

namespace MortgageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;
        private readonly IContext _context;
        private readonly IService<LeadsDto> _service;
        private readonly IMapper _mapper;


        public EmailController(IEmailService emailService,IContext context, IService<LeadsDto> service, IMapper mapper)
        {
            _emailService = emailService;
            _context = context;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("send-magic-link")]//[FromBody] string email,
        public async Task<IActionResult> SendMagicLink(int id)
        {
            var token = GenerateToken();
            var lead = await _service.GetAsync(id);
            lead.Token = token;
            lead.Expiration = DateTime.UtcNow.AddMinutes(15);
            await _service.UpdateAsync(id, _mapper.Map<LeadsDto>(lead));
            await _emailService.SendMagicLink(lead.Email, token,id);
            return Ok("Magic link sent.");
        }

        private string GenerateToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }


        [HttpGet("validate-magic-link")]
        public async Task<IActionResult> ValidateMagicLink(int id)
        {
            var lead = await _service.GetAsync(id);
            var isValid = await IsTokenValid(lead);
            if (isValid)
            {
                return Ok("Token is valid.");
            }
            return BadRequest("Token is invalid or expired.");
        }
        
        private async Task<bool> IsTokenValid(LeadsDto lead)
        {
            if (lead == null || lead.Expiration < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }
}
