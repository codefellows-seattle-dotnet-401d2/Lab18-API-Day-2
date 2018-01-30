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
    public class UnitTest2
    {
        TaskListDbContext _context;

        DbContextOptions<TaskListDbContext> options = new DbContextOptionsBuilder<TaskListDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        [Fact]
        public void GetAllLists()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<ListClass> testLists = GetTestLists();
                foreach (ListClass x in testLists)
                {
                    _context.Lists2.Add(x);
                }
                _context.SaveChanges();

                // Arrange
                ListController controller = new ListController(_context);
                // Act
                List<ListClass> result = controller.Get().ToList();
                // Assert
                Assert.Equal(testLists.Count, result.Count);
            }
        }

        [Fact]
        public void GetSingleList()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<ListClass> testLists = GetTestLists();
                foreach (ListClass x in testLists)
                {
                    _context.Lists2.Add(x);
                }
                List<TaskClass> testTasks = GetTestTasks();
                foreach (TaskClass x in testTasks)
                {
                    _context.Tasks2.Add(x);
                }
                _context.SaveChanges();

                // Arrange
                ListController controller = new ListController(_context);
                // Act
                ListClass result = controller.Get(2);
                // Assert
                Assert.Equal(testLists[1], result);
            }
        }

        [Fact]
        public void PostList()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<ListClass> testLists = GetTestLists();
                foreach (ListClass x in testLists)
                {
                    _context.Lists2.Add(x);
                }
                _context.SaveChanges();

                ListClass newList = new ListClass
                {
                    Id = 3,
                    Name = "List 3",
                };
                testLists.Add(newList);

                // Arrange
                ListController controller = new ListController(_context);
                // Act
                Task<IActionResult> resultRequest = controller.Post(newList);
                ListClass resultGet = controller.Get(3);
                // Assert
                Assert.NotNull(resultRequest);
                Assert.Equal(testLists[2], resultGet);
            }
        }

        [Fact]
        public void PutList()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<ListClass> testLists = GetTestLists();
                foreach (ListClass x in testLists)
                {
                    _context.Lists2.Add(x);
                }
                _context.SaveChanges();
                testLists[0].Name = "Mega List!";

                // Arrange
                ListController controller = new ListController(_context);
                // Act
                Task<IActionResult> resultRequest = controller.Put(1, testLists[0]);
                ListClass resultGet = controller.Get(1);
                // Assert
                Assert.NotNull(resultRequest);
                Assert.Equal(testLists[0], resultGet);
            }
        }

        [Fact]
        public void DeleteList()
        {
            using (_context = new TaskListDbContext(options))
            {
                List<ListClass> testLists = GetTestLists();
                foreach (ListClass x in testLists)
                {
                    _context.Lists2.Add(x);
                }
                List<TaskClass> testTasks = GetTestTasks();
                foreach (TaskClass x in testTasks)
                {
                    _context.Tasks2.Add(x);
                }
                _context.SaveChanges();

                // Arrange
                ListController controller = new ListController(_context);
                // Act
                Task<IActionResult> resultRequest = controller.Delete(1);
                var result = controller.Get(1);
                TaskClass resultTask1 = _context.Tasks2.First(x => x.Id == 1);
                TaskClass resultTask2 = _context.Tasks2.First(x => x.Id == 2);
                // Assert
                Assert.Null(result);
                Assert.Null(resultTask1.ListClassId);
                Assert.Null(resultTask2.ListClassId);
            }
        }

        private List<ListClass> GetTestLists()
        {
            var testLists = new List<ListClass>
            {
                new ListClass { Id = 1, Name = "Task 1" },
                new ListClass { Id = 2, Name = "Task 2" },
            };

            return testLists;
        }

        private List<TaskClass> GetTestTasks()
        {
            var testTasks = new List<TaskClass>
            {
                new TaskClass { Id = 1, Title = "Task 1", Notes = "1 is peaceful", ListClassId = 1 },
                new TaskClass { Id = 2, Title = "Task 2", Notes = "2 is a dynamic duo", ListClassId = 1 },
                new TaskClass { Id = 3, Title = "Task 3", Notes = "3's a crowd", ListClassId = 2 },
                new TaskClass { Id = 4, Title = "Task 4", Notes = "4 is a party", ListClassId = 2 }
            };

            return testTasks;
        }
    }
}
