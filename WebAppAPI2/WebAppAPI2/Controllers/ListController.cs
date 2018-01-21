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
    public class ListController : Controller
    {
        private readonly TaskListDbContext _context;

        public ListController(TaskListDbContext context)
        {
            _context = context;
        }
        
        // GET: api/<controller>/
        [HttpGet]
        public IEnumerable<ListClass> Get()
        {
            return _context.Lists2;
        }

        // GET: api/<controller>/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            ListClass myList = _context.Lists2.FirstOrDefault(x => x.Id == id);
            if(myList != null)
            {
                myList.TaskList = _context.Tasks2.Where(x => x.ListClassId == id).ToList();
                return Ok(myList);
            }
            else return BadRequest();
        }

        // POST: api/<controller>/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ListClass value)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            await _context.Lists2.AddAsync(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { value.Id }, value);
        }

        // PUT: api/<controller>/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]ListClass value)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            value.Id = id;
            if (_context.Lists2.FirstOrDefault(x => x.Id == id) != null)
            {
                _context.Lists2.Update(value);
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
            ListClass deleteList = _context.Lists2.FirstOrDefault(x => x.Id == id);
            if (deleteList != null)
            {
                deleteList.TaskList = _context.Tasks2.Where(x => x.ListClassId == id).ToList();
                foreach(TaskClass t in deleteList.TaskList)
                {
                    t.ListClassId = null;
                    _context.Tasks2.Update(t);
                }
                _context.Lists2.Remove(deleteList);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
