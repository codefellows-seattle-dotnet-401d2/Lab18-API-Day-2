using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class TaskItem
    {
        //Unique ID
        public int? Id { get; set; }

        //Timestamp when the TaskItem is created
        public long Created { get; }

        //Optional "Must be complete by" timestamp
        public long DueBy { get; set; }

        //Optional time to remind the user
        public long RemindAt { get; set; }

        //Required description
        [Required]
        public string Description { get; set; }

        public TaskItem()
        {
            Id = null;
            Created = ToUnixTimestampTicks();
        }
        
        public static long ToUnixTimestampTicks() => new DateTime().ToUniversalTime().Ticks - UnixEpochTicks;
        private static readonly long UnixEpochTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
    }
}
