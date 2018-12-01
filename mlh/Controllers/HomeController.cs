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

    public class HomeController : Controller
    {


        //admin area
        db context = new db();
        [HttpGet,Route("getusers")]
        public async Task<object> getusers(){
            var users = await context.users.ToListAsync();
            return users;
        }

        [HttpGet, Route("gettutors")]
        public async Task<object> gettutors()
        {
            var users = await context.users.Where(x=>x.services.Length>3).ToListAsync();
            return users;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("https://mlh-team-hahaha.azurewebsites.net/Tutor.html");
        }

        [HttpGet, Route("getcourses")]
        public async Task<object> getcourses()
        {
            return await context.courses.ToListAsync();
        }
    }
}
