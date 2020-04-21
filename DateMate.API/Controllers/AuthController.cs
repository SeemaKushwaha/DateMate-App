using DateMate.API.Data;
using Microsoft.AspNetCore.MVC;
using System.Threading.Tasks;

namespace DateMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username,string password)
        {
            //validate the request

            username = username.ToLower();

            if(await _repo.UserExists(username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                Username = username

            }

            var createdUser = await _repo.Register(userToCreate, password);

            //return CreatedAtRoute();
            return StatusCode(201);
        }
    }
}