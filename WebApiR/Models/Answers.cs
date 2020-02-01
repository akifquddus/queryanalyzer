using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiR.Models
{
    public class Answers
    {
        [Key]
        public int AnswerId { get; set; }
        
        [Required]
        public int QuestionId { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
