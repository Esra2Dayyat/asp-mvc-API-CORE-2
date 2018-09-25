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
    public class SearchController : ControllerBase
    {

        private readonly UserContext _Context;

        public SearchController(UserContext Context)
        {
            _Context = Context;
        }
        // search by id 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> finduser([FromRoute] int id) {

            User user = await _Context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            return Ok(user);



        }
        [HttpGet]
        public async Task<IActionResult> FindUsers( [FromQuery] string  name )
        {

           List <User> user =  _Context.Users.Where(x=>x.UserName == name).ToList();

            if (user == null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            return Ok(user);



        }



    }
}