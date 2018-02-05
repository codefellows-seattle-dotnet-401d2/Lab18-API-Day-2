using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaster.Data
{
    public class TaskCategoryAssocDbContext : DbContext
    {
        public DbSet<Models.TaskCategoryAssoc> Associations { get; set; }

        public TaskCategoryAssocDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<TaskCategoryAssocDbContext> options) : base(options)
        {

        }
    }
}
