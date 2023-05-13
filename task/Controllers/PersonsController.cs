
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task.Data;
using task.Models;

namespace task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public PersonsController(IAuthRepository repo)
        {
            _repo = repo;
        }

         [HttpPost("retrieve")]
        public async Task<IActionResult> retrieveData([FromBody] Person person){

                var user=await _repo.retrieveUserData(person);
                if(user!=null){

                    return Ok(user);
                }
                return  BadRequest("userExisting");
        
        }
            

        // POST api/values
        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] Person person)
        {       
            //validation 
                var user=await _repo.register(person);
                if(user!=null){

                    return Ok(user);
                }
                return  BadRequest("userExisting");
                 
        }
       
        [HttpPut("resetpassword")]
        public async Task<IActionResult> resetPassword([FromBody] Person person){

            var done=await _repo.resetUserPassword(person);
            if(done){
                    return StatusCode(201);
            }
            return  BadRequest("wrongPassword");
        }

    }
}
