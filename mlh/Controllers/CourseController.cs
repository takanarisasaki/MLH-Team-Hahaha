using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mlh.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mlh.Controllers
{
    [Route("course")]
    public class CourseController : Controller
    {
        [HttpPost,Route("addcourse")]
        public async Task<object> addcourse([FromBody]addcourse data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


        }
    }
}
