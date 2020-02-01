using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApiR.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public Array CategoryIds;

        public string Image { get; set; }
    }
}
