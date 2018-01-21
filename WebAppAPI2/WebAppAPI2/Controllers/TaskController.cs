using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI2.Models;
using WebAppAPI2.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppAPI2.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskListDbContext _context;

        public TaskController(TaskListDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>/
        [HttpGet]
        public IEnumerable<TaskClass> Get()
        {
            return _context.Tasks;
        }

        // GET: api/<controller>/{id}
        [HttpGet("{id:int}")]
        public TaskClass Get(int id)
        {
            return _context.Tasks.FirstOrDefault(x => x.Id == id);
        }

        // POST: api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskClass value)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            value.Time = DateTime.Now;
            await _context.Tasks.AddAsync(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { value.Id }, value);
        }

        // PUT: api/<controller>/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]TaskClass value)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            value.Id = id;
            value.Time = DateTime.Now;
            if(_context.Tasks.FirstOrDefault(x => x.Id == id) != null)
            {
                _context.Tasks.Update(value);
                await _context.SaveChangesAsync();
            }
            else
            {
                await Post(value);
            }
            return Ok();
        }

        // DELETE: api/<controller>/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            TaskClass deleteTask = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (deleteTask != null)
            {
                _context.Tasks.Remove(deleteTask);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
