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
    [Produces("application/json")]
    [Route("api/Association")]
    public class AssociationController : Controller
    {
        private readonly TaskItemsDbContext _context;

        //Constructor requires context in order to instantiate TaskItemController.
        public AssociationController(TaskItemsDbContext context)
        {
            _context = context;
        }

        //Create COMPLETE
        [HttpPost]
        public async Task<IActionResult> PostAsync(int taskId, int categoryId)
        {
            TaskCategoryAssoc association = new TaskCategoryAssoc { TaskItem = taskId, Category = categoryId };

            if (!_context.Associations.Contains(association))
            {
                await _context.Associations.AddAsync(association);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(actionName: "Get", value: association);
        }

        //Delete COMPLETE
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int taskId, int categoryId)
        {
            TaskCategoryAssoc association = new TaskCategoryAssoc { TaskItem = taskId, Category = categoryId };

            if (_context.Associations.Contains(association))
            {
                _context.Associations.Remove(association);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(actionName: "Delete", value: association);
        }
    }
}