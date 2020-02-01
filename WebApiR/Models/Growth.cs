using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiR.Models
{
    public class Growth
    {
        [Key]
        public int GrowthId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public Double Weight { get; set; }

        [Required]
        public Double Height { get; set; }

        [Required]
        public Double Head { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
