using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaster.Models
{
    public class Category
    {
        //Unique Id
        public int Id { get; set; }

        //required name
        [Required]
        public string Name { get; set; }

        //Auto-filling list of all task items within the category
        [NotMapped]
        public List<TaskItem> Tasks { get; set; }

    }
}
