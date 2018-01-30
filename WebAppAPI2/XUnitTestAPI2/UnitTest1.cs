using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using WebAppAPI2.Models;
using WebAppAPI2.Controllers;
using WebAppAPI2.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace XUnitTestAPI2
{
    public class UnitTest1
    {
        TaskListDbContext _context;

        DbContextOptions<TaskListDbContext> options = new DbContextOptionsBuilder<TaskListDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        [Fact]
        public void GetAllTasks()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<TaskClass> testTasks = GetTestTasks();
                foreach (TaskClass x in testTasks)
                {
                    _context.Tasks2.Add(x);
                }
                _context.SaveChanges();
                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                IEnumerable<TaskClass> result = controller.Get();
                List<TaskClass> resultList = result.ToList();
                // Assert
                Assert.Equal(testTasks.Count, resultList.Count);
            }
        }

        [Fact]
        public void GetSingleTask()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<TaskClass> testTasks = GetTestTasks();
                foreach (TaskClass x in testTasks)
                {
                    _context.Tasks2.Add(x);
                }
                _context.SaveChanges();
                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                TaskClass result = controller.Get(2);
                // Assert
                Assert.Equal(testTasks[1], result);
            }
        }

        [Fact]
        public void PostTask()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<TaskClass> testTasks = GetTestTasks();
                foreach (TaskClass x in testTasks)
                {
                    _context.Tasks2.Add(x);
                }
                TaskClass newTask = new TaskClass
                {
                    Id = 5,
                    Title = "Task 5",
                    Notes = "5 is a rave"
                };
                testTasks.Add(newTask);
                _context.SaveChanges();

                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                Task<IActionResult> resultRequest = controller.Post(newTask);
                TaskClass resultGet = controller.Get(5);
                // Assert
                Assert.NotNull(resultRequest);
                Assert.Equal(testTasks[4], resultGet);
            }
        }

        [Fact]
        public void PutTask()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<TaskClass> testTasks = GetTestTasks();
                foreach (TaskClass x in testTasks)
                {
                    _context.Tasks2.Add(x);
                }
                _context.SaveChanges();
                testTasks[0].Title = "Eek Help! Oh no, Molly.";

                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                Task<IActionResult> resultRequest = controller.Put(1, testTasks[0]);
                TaskClass resultGet = controller.Get(1);
                // Assert
                Assert.Equal(testTasks[0], resultGet);
            }
        }

        [Fact]
        public void DeleteTask()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<TaskClass> testTasks = GetTestTasks();
                foreach (TaskClass x in testTasks)
                {
                    _context.Tasks2.Add(x);
                }
                _context.SaveChanges();
                // Arrange
                TaskController controller = new TaskController(_context);
                // Act
                Task<IActionResult> resultRequest = controller.Delete(2);
                var result = controller.Get(2);
                // Assert
                Assert.Null(result);
            }
        }

        private List<TaskClass> GetTestTasks()
        {
            var testTasks = new List<TaskClass>
            {
                new TaskClass { Id = 1, Title = "Task 1", Notes = "1 is peaceful" },
                new TaskClass { Id = 2, Title = "Task 2", Notes = "2 is a dynamic duo" },
                new TaskClass { Id = 3, Title = "Task 3", Notes = "3's a crowd" },
                new TaskClass { Id = 4, Title = "Task 4", Notes = "4 is a party" }
            };

            return testTasks;
        }
    }
}
