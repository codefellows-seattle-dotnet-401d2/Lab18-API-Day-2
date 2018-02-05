using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaster.Data
{
    public class CategoriesDbContext : DbContext
    {
        public DbSet<Models.Category> Categories { get; set; }

        public CategoriesDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<CategoriesDbContext> options) : base(options)
        {

        }
    }
}
