using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mlh.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mlh.Controllers
{
    [Route("api/test")]
    public class ValuesController : Controller
    {
        db context = new db();
        // GET: api/values
        [HttpGet,Route("api/test/maintest")]
        public object Get()
        {
            return context.tests.Where(x=>true).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            context.tests.Add(new test
            {
                name = id
            });
            await context.SaveChangesAsync();
            return "success";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
