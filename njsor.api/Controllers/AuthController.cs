using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using njsor.api.Data;
using njsor.api.Dtos;
using njsor.api.Model;

namespace njsor.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration  config )
        {
           _repo = repo;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForDto userForDto)
        {
          userForDto.Username=userForDto.Username.ToLower();

          if(await _repo.UserExist(userForDto.Username))
          return BadRequest("User already exist");

          var usertocreate = new User
          {
              Username=userForDto.Username
          } ;  

          var createdUser = await _repo.Register(usertocreate,userForDto.Password);
          return StatusCode(201);
        }
          [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var usertoverify = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if(usertoverify == null)
            return Unauthorized();

            var claims= new []
            {
                new Claim(ClaimTypes.NameIdentifier, usertoverify.Id.ToString()),
                new Claim(ClaimTypes.Name,usertoverify.Username)   
                  };

            var key= new SymmetricSecurityKey((Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value))) ;  
//var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key")) ;
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDesciptor= new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesciptor);
            
            return Ok(new {
               token = tokenHandler.WriteToken(token)
            });
        }
    }
}