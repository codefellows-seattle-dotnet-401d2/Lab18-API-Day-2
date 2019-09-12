using Microsoft.EntityFrameworkCore;
using TaskMaster.Models;

namespace TaskMaster.Data
{
    public class TaskItemsDbContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TaskCategoryAssoc> Associations { get; set; }

        public TaskItemsDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<TaskItemsDbContext> options) : base(options)
        {

        }
    }
}
