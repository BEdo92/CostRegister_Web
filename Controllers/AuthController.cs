using CostRegApp2.Data;
using CostRegApp2.DTOs;
using CostRegApp2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CostRegApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _repo;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userDto)
        {
            var userNameToSave = userDto.UserName.ToLower();
            
            if (await _repo.AuthRepository.UserExists(userNameToSave))
            {
                return BadRequest("Username already exists!");
            }
            
            var userToCreate = new User
            {
                UserName = userNameToSave,
                Created = DateTime.Now
            };

            var createdUser = await _repo.AuthRepository.Register(userToCreate, userDto.Password);

            if (createdUser != null)
            {
                return StatusCode(201);
            }

            return BadRequest("New user was not created!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userDto)
        {
            var userFromRepo = await _repo.AuthRepository.Login(userDto.UserName, userDto.Password);

            if (userFromRepo is null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            }); 
        }
    }
}
