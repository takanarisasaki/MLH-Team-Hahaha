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
        [HttpGet,Route("randomdata")]
        public async Task<string> randomdata(){
            var l = new List<Course>();
            l.Add(new Course { name = "ecse321", description = "introduction to software engineering" });
            l.Add(new Course { name = "ecse429", description = "software validation" });
            l.Add(new Course { name = "comp251", description = "data structures and algorithms" });
            l.Add(new Course { name = "ecse211", description = "design principle and methods" });
            l.Add(new Course { name = "facc300", description = "engineering economy" });
            context.courses.AddRange(l);
            await context.SaveChangesAsync();
            var usrs = new List<User>();
            usrs.Add(new User
            {
                username = "gfdsafff",
                password = "fjeoajfo",
                availability = "Monday-Friday: 9am-5pm",
                firstname = "Joe",
                lastname = "Zhang",
                price = "30.99$/hr",
                rating = "3",
                description = "I graduated top of my class at Beijing. I'm a skilled expert at Mathematics",
                phone = "514539219",
                email = "joe@gmail.com"
            });
            usrs.Add(new User
            {
                username = "gjeaofhibj",
                password = "fjeoajfobuhinl",
                availability = "sfdsfsfdfds",
                firstname = "Alberta",
                lastname = "Lu",
                price = "41.99$/hr",
                rating = "5",
                description = "I teach anything extremely wellwell.",
                phone = "514471209",
                email = "jfjewo@gmail.com"
            });
            usrs.Add(new User
            {
                username = "gjeaof",
                password = "fjeoajfo",
                availability = "Anytime!",
                firstname = "Fredric",
                lastname = "Lam",
                price = "30.00$/hr",
                rating = "4",
                description = "I'm from alababa",
                phone = "514559219",
                email = "fredric@gmail.com"
            });
            usrs.Add(new User
            {
                username = "gjeaof",
                password = "fjeoajfo",
                availability = "I'm available any day.",
                firstname = "Blair",
                lastname = "Du",
                price = "100.21/hr",
                rating = "0",
                description = "Choose me no problem",
                phone = "514544219",
                email = "blair@gmail.com"
            });
            usrs.Add(new User
            {
                username = "xd",
                password = "fjeoajfo",
                availability = "I'm available on weekends.",
                firstname = "BRIAN",
                lastname = "LI",
                price = "55.21/hr",
                rating = "0",
                description = "im from al",
                phone = "514539219",
                email = "BRIANLI@gmail.com"
            });
            usrs[0].addservice(l[4].id.ToString());
            usrs[0].addservice(l[1].id.ToString());
            usrs[1].addservice(l[2].id.ToString());
            usrs[1].addservice(l[4].id.ToString());
            usrs[1].addservice(l[1].id.ToString());
            usrs[2].addservice(l[0].id.ToString());
            usrs[3].addservice(l[2].id.ToString());
            usrs[3].addservice(l[3].id.ToString());
            usrs[3].addservice(l[1].id.ToString());
            usrs[3].addservice(l[4].id.ToString());
            context.users.AddRange(usrs);
            await context.SaveChangesAsync();
            return "success";
        }
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
        [HttpPost,Route("getfullinfo")]
        public async Task<object> getfullinfo([FromBody]CreateUser data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await validate(data.username, data.password);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost,Route("updateinfo")]
        public async Task<object> update([FromBody]updateinfo data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await validate(data.username, data.password);
            if (user==null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(data.availability))
                user.availability = data.availability;
            if (!string.IsNullOrEmpty(data.firstname))
                user.firstname = data.firstname;
            if (!string.IsNullOrEmpty(data.lastname))
                user.lastname = data.lastname;
            if (!string.IsNullOrEmpty(data.price))
                user.price = data.price;
            if (!string.IsNullOrEmpty(data.rating))
                user.rating = data.rating;
            if (!string.IsNullOrEmpty(data.phone))
                user.phone = data.phone;
            if (!string.IsNullOrEmpty(data.email))
                user.email = data.email;

                context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return user;
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
