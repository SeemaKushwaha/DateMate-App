using System;
using DateMate.API.Data;
using DateMate.API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DateMate.API.Dtos;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DateMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User()
            {
                Username = userForRegisterDto.Username

            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            //return CreatedAtRoute();
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if(userFromRepo == null)
            {
                return Unauthorized();
            } 
             var claims = new[]
             {
                 new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                 new Claim(ClaimTypes.Name, userFromRepo.Username)
             };

             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}