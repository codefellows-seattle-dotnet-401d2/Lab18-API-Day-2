using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppAPI2.Models;

namespace WebAppAPI2.Data
{
    public class TaskListDbContext : DbContext
    {
        public TaskListDbContext(DbContextOptions<TaskListDbContext> options) : base(options)
        {

        }

        public DbSet<TaskClass> Tasks2 { get; set; }
        public DbSet<ListClass> Lists2 { get; set; }
    }
}
