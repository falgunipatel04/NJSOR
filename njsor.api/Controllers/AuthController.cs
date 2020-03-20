using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public AuthController(IAuthRepository repo)
        {
           _repo = repo;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForDtos userForDtos)
        {
          userForDtos.Username=userForDtos.Username.ToLower();

          if(await _repo.UserExist(userForDtos.Username))
          return BadRequest("User already exist");

          var usertocreate = new User
          {
              Username=userForDtos.Username
          } ;  

          var createdUser = await _repo.Register(usertocreate,userForDtos.Password);
          return StatusCode(201);
        }
    }
}