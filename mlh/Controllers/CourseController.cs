using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mlh.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mlh.Controllers
{
    [Route("course")]
    public class CourseController : Controller
    {
        db context = new db();
        [HttpPost,Route("addcourse")]
        public async Task<object> addcourse([FromBody]addcourse data){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Course course = new Course
            {
                name = data.name,
                description = data.description
            };
            context.courses.Add(course);
            await context.SaveChangesAsync();
            return course.GetPreview();
        }

        [HttpPost,Route("getcourselist")]
        public async Task<object> getlist(){
            var list = await context.courses.ToListAsync();
            return list.Select(x => x.GetPreview());
        }
        [HttpPost,Route("gettutors")]
        public async Task<object> gettutors([FromBody]gettutors data){
            var course = await context.courses.FindAsync(data.courseid);
            if (course==null)
            {
                return NotFound();
            }
            var id = data.courseid.ToString();
            var tutors = await context.users.Where(x => x.services.Contains(id)).ToListAsync();
            return tutors.Select(x => x.getpreview());
        }
    }


}
