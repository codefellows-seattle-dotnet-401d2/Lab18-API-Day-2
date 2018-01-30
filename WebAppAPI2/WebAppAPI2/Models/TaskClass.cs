using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppAPI2.Models
{
    public class TaskClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Notes { get; set; }

        public DateTime Time { get; set; }
        [ForeignKey("ListClassId")]
        public int? ListClassId { get; set; }
    }
}
