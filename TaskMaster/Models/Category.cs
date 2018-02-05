using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    }
}
