using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskMaster.Data;
using TaskMaster.Models;

namespace TaskMaster.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {

        private readonly TaskItemsDbContext _context;

        //Constructor requires context in order to instantiate TaskItemController.
        public CategoryController(TaskItemsDbContext context)
        {
            _context = context;
        }

        //Create COMPLETE
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string name)
        {
            Category category = new Category() { Name = name };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { category.Id }, category);
        }

        //Get all COMPLETE
        [HttpGet]
        public List<Category> Get()
        {
            List<Category> categorylist = _context.Categories.ToList();
            foreach (Category category in categorylist)
            {
                category.Tasks = new List<TaskItem>();

                List<TaskCategoryAssoc> associations = _context.Associations.Where(assoc => assoc.Category == category.Id).ToList();

                TaskItemController taskController = new TaskItemController(_context);

                // This code is beautiful and I love it.
                category.Tasks = taskController.Get().Where(
                    _task => associations.FirstOrDefault(
                        a => a.TaskItem == _task.Id
                    ) != null
                ).ToList();
                // *Mic drop*
            }
            return categorylist;
        }

        //Update COMPLETE
        public async Task<IActionResult> PutAsync([FromBody]Category _category)
        {
            if (_context.Categories.Where(category => category.Id == _category.Id).ToList().Count > 0)
            {
                Category cat = _context.Categories.FirstOrDefault(category => category.Id == _category.Id);
                _context.Categories.Update(cat);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new { cat.Id }, cat);
            }
            else
            {
                return await Post(JsonConvert.SerializeObject(_category));
            }
        }


        //Delete Complete
        [HttpDelete("{id:int}")]
        public StatusCodeResult Delete(int Id)
        {
            if (_context.Categories.Where(category => category.Id == Id).ToList().Count > 0)
            {
                Category cat = _context.Categories.FirstOrDefault(category => category.Id == Id);
                _context.Categories.Remove(cat);
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