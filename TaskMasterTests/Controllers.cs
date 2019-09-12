using System;
using Xunit;
using TaskMaster.Models;
using TaskMaster.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskMaster.Data;
using System.Collections.Generic;

namespace TaskMasterTests
{
    public class ControllerTest
    {
        [Fact]
        public void TestBlueprint()
        {
            TaskItemsDbContext _context;

            DbContextOptions<TaskItemsDbContext> options = new DbContextOptionsBuilder<TaskItemsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (_context = new TaskItemsDbContext(options))
            {
                List<TaskItem> startinglist = GetTestTasks();

                foreach (TaskItem task in startinglist)
                {
                    _context.TaskItems.Add(task);
                }
                _context.SaveChangesAsync();

                TaskItemController taskController = new TaskItemController(_context);

                // HERE GOES ACTUAL TEST
            }
        }

        [Fact]
        public void TestGetAll()
        {
            TaskItemsDbContext _context;

            DbContextOptions<TaskItemsDbContext> options = new DbContextOptionsBuilder<TaskItemsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (_context = new TaskItemsDbContext(options))
            {
                List<TaskItem> startinglist = GetTestTasks();

                foreach(TaskItem task in startinglist)
                {
                    _context.TaskItems.Add(task);
                }
                _context.SaveChangesAsync();

                TaskItemController taskController = new TaskItemController(_context);

                List<TaskItem> tasklist = taskController.Get();

                Assert.Equal(2, tasklist.Count);

            }
        }

        [Fact]
        public void TestGetOne()
        {
            TaskItemsDbContext _context;

            DbContextOptions<TaskItemsDbContext> options = new DbContextOptionsBuilder<TaskItemsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (_context = new TaskItemsDbContext(options))
            {
                List<TaskItem> startinglist = GetTestTasks();

                foreach (TaskItem task in startinglist)
                {
                    _context.TaskItems.Add(task);
                }
                _context.SaveChangesAsync();

                TaskItemController taskController = new TaskItemController(_context);

                List<TaskItem> tasklist = taskController.Get();

                TaskItem item1 = taskController.Get(1);

                Assert.Matches("Make bean dip for superbowl", item1.Description);
            }
        }

        // NOT TESTS

        private List<TaskItem> GetTestTasks()
        {
            var testMaterials = new List<TaskItem>
            {
                new TaskItem
                {
                    Description = "Make bean dip for superbowl"
                },
                new TaskItem
                {
                    Description = "Eat said bean dip and leave none for family and friends.",
                    DueBy = 123123123,
                    RemindAt = 123000000
                }
            };

            return testMaterials;
        }
    }
}
