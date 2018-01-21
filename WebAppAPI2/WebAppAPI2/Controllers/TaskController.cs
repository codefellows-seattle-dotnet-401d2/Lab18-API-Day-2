using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI2.Models;
using WebAppAPI2.Data;

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
            return _context.Tasks2;
        }

        // GET: api/<controller>/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            TaskClass myTask = _context.Tasks2.FirstOrDefault(x => x.Id == id);
            if (myTask != null) return Ok(myTask);
            else return BadRequest();
        }

        // POST: api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskClass value)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            value.Time = DateTime.Now;
            await _context.Tasks2.AddAsync(value);
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
            if(_context.Tasks2.FirstOrDefault(x => x.Id == id) != null)
            {
                _context.Tasks2.Update(value);
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
            TaskClass deleteTask = _context.Tasks2.FirstOrDefault(x => x.Id == id);
            if (deleteTask != null)
            {
                _context.Tasks2.Remove(deleteTask);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
