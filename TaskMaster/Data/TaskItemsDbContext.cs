using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaster.Data
{
    public class TaskItemsDbContext : DbContext
    {
        public DbSet<Models.TaskItem> TaskItems { get; set; }

        public TaskItemsDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<TaskItemsDbContext> options) : base(options)
        {

        }
    }
}
