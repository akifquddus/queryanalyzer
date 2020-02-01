using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR
{
    public class WebApiRDbContext : DbContext
    {
        public WebApiRDbContext(DbContextOptions<WebApiRDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answers> Answers { get; set; }

        public DbSet<Growth> Growth { get; set; }
    }
}
