using System;
using TaskMaster.Models;
using Xunit;

namespace TaskMasterTests
{
    public class ModelTest
    {

        [Fact]
        public void GetId()
        {
            TaskItem task = NewTask();
            Assert.Equal(1, task.Id);
        }

        [Fact]
        public void GetCreated()
        {
            TaskItem task = NewTask();
            Assert.IsType<long>(task.Created);
        }

        [Fact]
        public void GetDueBy()
        {
            TaskItem task = NewTask();
            Assert.IsType<long>(task.DueBy);
        }

        [Fact]
        public void GetRemindAt()
        {
            TaskItem task = NewTask();
            Assert.IsType<long>(task.RemindAt);
        }

        [Fact]
        public void GetDescription()
        {
            TaskItem task = NewTask();
            Assert.IsType<string>(task.Description);
        }

        [Fact]
        public void SetId()
        {
            TaskItem task = NewTask();
            task.Id = 2;
            Assert.Equal(2, task.Id);
        }


        [Fact]
        public void SetDueBy()
        {
            TaskItem task = NewTask();
            task.DueBy = 123123123;
            Assert.Equal(123123123, task.DueBy);
        }

        [Fact]
        public void SetRemindAt()
        {
            TaskItem task = NewTask();
            task.RemindAt = 123123123;
            Assert.Equal(123123123, task.RemindAt);
        }

        [Fact]
        public void SetDescription()
        {
            TaskItem task = NewTask();
            task.Description = "Cheese";
            Assert.Matches("Cheese", task.Description);
        }

        private static TaskItem NewTask()
        {
            TaskItem newtaskitem = new TaskItem()
            {
                Id = 1,
                DueBy = TaskItem.ToUnixTimestampTicks() + 1000 * 60 * 30,
                RemindAt = TaskItem.ToUnixTimestampTicks() + 1000 * 60 * 20,
                Description = "A task for tasking."
            };

            return newtaskitem;
        }
    }
}
