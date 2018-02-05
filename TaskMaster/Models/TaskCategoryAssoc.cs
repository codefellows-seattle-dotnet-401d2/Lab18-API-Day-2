using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class TaskCategoryAssoc
    {
        //Unique Id
        public int Id { get; set; }

        //Associated Category Id
        [Required]
        public int Category { get; set; }

        //Associated Task Item Id
        [Required]
        public int TaskItem { get; set; }
    }
}
