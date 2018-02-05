using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Data;
using TaskMaster.Models;

namespace TaskMaster.Controllers
{
    [Route("api/[controller]")]
    public class TaskItemController : Controller
    {

        private readonly TaskItemsDbContext _context;

        //Constructor requires context in order to instantiate TaskItemController.
        public TaskItemController(TaskItemsDbContext context)
        {
            _context = context;
        }

        //Create todo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskItem taskItem)
        {
            await _context.TaskItems.AddAsync(taskItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { taskItem.Id }, taskItem);
        } 

        //Get all COMPLETE
        [HttpGet]
        public List<TaskItem> Get() => _context.TaskItems.ToList();

        //Get one todo
        [HttpGet("{id:int}")]
        public TaskItem Get(int Id)
        {
            return _context.TaskItems.FirstOrDefault(taskitem => taskitem.Id == Id);
        }

        //Update todo
        public StatusCodeResult Put([FromBody]TaskItem taskItem)
        {
            if(_context.TaskItems.Where(task => task.Id == taskItem.Id).ToList().Count > 0)
            {
                TaskItem task_item = _context.TaskItems.FirstOrDefault(taskitem => taskitem.Id == taskItem.Id);
                _context.TaskItems.Update(task_item);
                _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return StatusCode(404);
            }
        }


        //Delete todo
        [HttpDelete("{id:int}")]
        public StatusCodeResult Delete(int Id)
        {
            if (_context.TaskItems.Where(taskitem => taskitem.Id == Id).ToList().Count > 0)
            {
                TaskItem taskItem = _context.TaskItems.FirstOrDefault(taskitem => taskitem.Id == Id);
                _context.TaskItems.Remove(taskItem);
                _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return StatusCode(404);
            }
        }
    }
}