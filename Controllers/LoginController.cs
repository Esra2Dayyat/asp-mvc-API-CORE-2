using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DontCoreApiAuthantaication.Data;

namespace DontCoreApiAuthantaication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {    private readonly UserContext _Context; 

        public LoginController (UserContext  context )
        {
            _Context = context; 
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User userLogin = _Context.Users.Where(x => x.Email == user.Email && x.Password == user.Password ).FirstOrDefault();
             
            if (userLogin == null)
            {
                return NotFound();
            }

            return Ok(userLogin);


        }


    }
}