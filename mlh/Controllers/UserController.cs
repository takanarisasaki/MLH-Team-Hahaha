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

        [HttpPost,Route("addservice")]
        public async Task<object> addcourse([FromBody]addservice data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await validate(data.username, data.password);
            var course = await context.courses.FindAsync(data.courseid);
            if (course==null)
            {
                return NotFound();
            }
            user.addservice(data.courseid.ToString());
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return user.getpreview();
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

        [HttpPost,Route("gettutees")]
        public async Task<object> gettutees([FromBody]CreateUser data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await validate(data.username, data.password);
            return user.gettutees();
        }
        [HttpPost,Route("gettutorinfo/{id}")]
        public async Task<object> gettutorinfo(string id){
            Guid guid = Guid.Parse(id);
            var tutor = await context.users.FindAsync(guid);
            if (tutor==null)
            {
                return NotFound();
            }
            return new
            {
                id = tutor.username,
                phone = tutor.phone,
                email = tutor.email,
                services = tutor.getservices()
            };
        }
        public async Task<User> validate(string username,string password){
            return (await context.users.Where(x => x.username == username && x.password == password).ToListAsync()).FirstOrDefault();
        }
    }
}
