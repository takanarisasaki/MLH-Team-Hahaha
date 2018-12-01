using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mlh.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mlh.Controllers
{
    [Route("Users/")]
    public class UserController : Controller
    {
        db context = new db();
        [HttpPost,Route("createUser")]
        public async Task<object> createUser([FromBody]CreateUser data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new User
            {
                username = data.username,
                password = data.password
            };
            context.users.Add(user);
            await context.SaveChangesAsync();
            return user.getpreview();
        }

        [HttpPost,Route("login")]
        public async Task<object> login([FromBody]CreateUser data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user =await validate(data.username, data.password);
            if (user==null)
            {
                user = new User
                {
                    username = data.username,
                    password = data.password
                };
                context.users.Add(user);
                await context.SaveChangesAsync();
            }
            return user.getpreview();
        }
        [HttpPost,Route("addtutor")]
        public async Task<object> addtutor([FromBody]assignment data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await validate(data.username,data.password);
            var tutor = await context.users.FindAsync(data.userid);
            var course = await context.courses.FindAsync(data.courseid);
            if (user==null||tutor==null||course==null)
            {
                return NotFound();
            }
            user.addtutor(tutor.id.ToString(), course.id.ToString());
            tutor.addtutees(user.id.ToString(), course.id.ToString());
            context.Entry(user).State = EntityState.Modified;
            context.Entry(tutor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return user.getpreview();
        }

        public async Task<object> addcourse([FromBody]addservice data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await validate(data.username, data.password);
        }




        [HttpPost,Route("updateavailability")]
        public async Task<object> updateavailability([FromBody]availability data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await validate(data.username, data.password);
            if (user==null)
            {
                return NotFound();
            }
            user.availability = data.schedule;
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return user.getpreview();
        }

        

        public async Task<User> validate(string username,string password){
            return (await context.users.Where(x => x.username == username && x.password == password).ToListAsync()).FirstOrDefault();
        }
    }
}
