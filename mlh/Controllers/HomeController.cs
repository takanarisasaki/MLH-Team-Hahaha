using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mlh.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace mlh.Controllers
{
    [Route("manager")]
    public class HomeController : Controller
    {
        //admin area
        db context = new db();
        [HttpGet,Route("getusers")]
        public async Task<object> getusers(){
            var users = await context.users.ToListAsync();
            return users;
        }

        [HttpGet, Route("getcourses")]
        public async Task<object> getcourses()
        {
            return await context.courses.ToListAsync();
        }
    }
}
