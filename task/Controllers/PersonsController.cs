using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using task.Data;
using task.Models;

namespace task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ILogger<PersonsController> logger;
        public PersonsController(IAuthRepository repo, ILogger<PersonsController> logger)
        {
            this.logger = logger;
            _repo = repo;
        }

        [HttpPost("retrieve")]
        public async Task<Response> retrieveData([FromBody] userLogin person)
        {
            return await _repo.retrieveUserData(person);
        }


        // POST api/register
        [HttpPost("register")]
        public async Task<Response> register([FromBody] Person person)
        {
            return await _repo.register(person);
        }

        [HttpPut("resetpassword")]
        public async Task<Response> resetPassword([FromBody] passwordReset person)
        {
            return await _repo.resetUserPassword(person);
        }

    }
}
