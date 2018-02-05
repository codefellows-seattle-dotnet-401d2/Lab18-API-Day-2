using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Data;
using TaskMaster.Models;

namespace TaskMaster.Controllers
{
    [Route("api/[controller]")]
    public class TaskCategoryAssocController : Controller
    {

        private readonly TaskCategoryAssocDbContext _context;

        //Constructor requires context in order to instantiate TaskItemController.
        public TaskCategoryAssocController(TaskCategoryAssocDbContext context)
        {
            _context = context;
        }

        //Create Complete
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskCategoryAssoc association)
        {
            await _context.Associations.AddAsync(association);
            await _context.SaveChangesAsync();
            return Ok();
        } 

        //Get all COMPLETE
        [HttpGet]
        public List<TaskCategoryAssoc> Get() => _context.Associations.ToList();

        //Get all Categories for a task
        [HttpGet("{id:int}")]
        public List<Category> GetCategory(int TaskItemId)
        {
            DbContextOptions<CategoriesDbContext> options = new DbContextOptionsBuilder<CategoriesDbContext>().Options;
            CategoriesDbContext catContext = new CategoriesDbContext(options);
            CategoryController catController = new CategoryController(catContext);
            List<Category> catlist = new List<Category>();
            List<TaskCategoryAssoc> associations = _context.Associations.Where(association => association.TaskItem == TaskItemId).ToList();
            foreach(TaskCategoryAssoc assoc in associations)
            {
                catlist.Add(catController.Get().FirstOrDefault(cat => cat.Id == assoc.Category));
            }
            return (catlist);
        }

        //Get all tasks in a category
        [HttpGet("{id:int}")]
        public List<TaskItem> GetTasks(int CategoryId)
        {
            DbContextOptions<TaskItemsDbContext> options = new DbContextOptionsBuilder<TaskItemsDbContext>().Options;
            TaskItemsDbContext taskContext = new TaskItemsDbContext(options);
            TaskItemController taskController = new TaskItemController(taskContext);
            List<TaskItem> tasklist = new List<TaskItem>();
            List<TaskCategoryAssoc> associations = _context.Associations.Where(association => association.Category == CategoryId).ToList();
            foreach (TaskCategoryAssoc assoc in associations)
            {
                tasklist.Add(taskController.Get().FirstOrDefault(task => task.Id == assoc.TaskItem));
            }
            return (tasklist);
        }

        //Delete Complete
        [HttpDelete("{id:int}")]
        public StatusCodeResult Delete([FromBody]TaskCategoryAssoc association)
        {
            if (_context.Associations.Where(_association => _association.Category == association.Category && _association.TaskItem == association.TaskItem).ToList().Count > 0)
            {
                List<TaskCategoryAssoc> associations = _context.Associations.Where(_association => _association.Category == association.Category && _association.TaskItem == association.TaskItem).ToList();
                foreach (TaskCategoryAssoc assoc in associations)
                {
                    _context.Associations.Remove(assoc);
                }
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